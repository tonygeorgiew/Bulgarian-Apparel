/**
 * Created by YAO on 2016/12/15.
 * ========================================
 *
 */
(function (global, factory) {
    "use strict";

    if (typeof module === "object" && typeof module.exports === "object") {

        // For CommonJS and CommonJS-like environments where a proper `window` is present
        module.exports = global.document ? factory(global, true) : function (w) {

                if (!w.document) {
                    throw new Error("Pagination requires a window with a document");
                }

                return factory(w);
            };
    }
    else {
        // CMD (Register as an anonymous module)
        if ("function" == typeof define && define.cmd) {
            define(function (require, exports, module) {
                module.exports = factory(global, require('jquery'));
            });
        }
        else {
            // AMD
            if (typeof define === "function" && define.amd) {
                define('pagination', ['jquery'], factory(global, $));
            }
            else {
                factory(global, jQuery);
            }
        }
    }
// Pass this if window is not defined yet
})(typeof window !== "undefined" ? window : this, (function (window, $, noGlobal) {
    "use strict";

    var CLS_WRAP = 'pagination',
        CLS_LIST = 'list',
        CLS_PAGE = 'page',
        CLS_ELLIPSIS = 'ellipsis',
        CLS_PAGES = 'pages',
        CLS_NAV = 'nav',
        CLS_CURRENT = 'current',
        CLS_INDEX = 'index',
        CLS_PREV = 'prev',
        CLS_NEXT = 'next',
        CLS_FIRST = 'first',
        CLS_LAST = 'last',
        CLS_HIDDEN = 'hidden',
        CLS_INVISIBLE = 'invisible',
        CLS_DISABLED = 'disabled',
        CLS_CENTER = 'center',
        CLS_RIGHT = 'right',
        DOT = '.',
        HTML_CHARS = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#x27;',
            '/': '&#x2F;',
            '`': '&#x60;'
        },
        SCRIPT_FRAGMENT = '<script[^>]*>([\\S\\s]*?)<\/script\\s*>',
        _uid = -1,
        isFunction = $.isFunction;

    function guid(prefix) {
        var id;

        _uid += 1;
        id = prefix ? prefix + '-' + _uid : _uid;

        return id;
    }

    function stripScripts(html) {
        return html.replace(new RegExp(SCRIPT_FRAGMENT, 'img'), '');
    }

    function encodeHTML(html) {
        html = '' + html;

        return stripScripts(html).replace(/[\r\t\n]/g, ' ').replace(/[&<>"'\/`]/g, function (match) {
            return HTML_CHARS[match];
        });
    }

    function decodeHTML(html) {
        return html.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, '&').replace(/&quot;/g, '"').replace(/&#x27;/g, '\'').replace(/&#x2F;/g, '\/').replace(/&#x60;/g, '`');
    }

    function template(json, html) {
        html = '' + html;

        $.each(json, function (key, value) {
            html = html.replace(new RegExp('{' + key + '}', 'ig'), decodeHTML(encodeHTML(value)));
        });

        return html;
    }

    function isNumber(num) {
        return typeof num === 'number';
    }

    var Pagination = function (config) {
        this.attributes = {};

        this.parent = null;
        this.wrap = null;
        this.list = null;

        this.totalPage = 0;
        this.currentPage = 0;
        this.pageSize = 0;

        this.disabled = false;

        this.set(Pagination.defaults);

        if ($.isPlainObject(config)) {
            this.init(config);
        }
    };

    Pagination.defaults = {
        parent: null,
        totalPage: 0,
        currentPage: 1,
        pageSize: 5,
        showPages: true,
        showPrev: true,
        showNext: true,
        showPageNumbers: true,
        alwaysShowPrevNext: true,
        prevText: 'Prev',
        nextText: 'Next',
        ellipsisText: '...',
        align: 'left',
        beforeInit: null,
        afterInit: null,
        beforeRender: null,
        afterRender: null,
        beforeDrawPage: null,
        afterDrawPage: null,
        WRAP: '<div class="' + CLS_WRAP + '" id="{id}"></div>',
        LIST: '<ul class="' + CLS_LIST + '"></ul>',
        PAGES: '<li class="' + CLS_PAGE + ' ' + CLS_PAGES + ' ' + CLS_FIRST + '"><span class="text"><em class="' + CLS_INDEX + '">{current}</em> / <strong class="total">{total}</strong> Pages</span></li>',
        PREV: '<li class="' + CLS_PAGE + ' ' + CLS_PREV + '"><a class="text">{prev}</a></li>',
        NEXT: '<li class="' + CLS_PAGE + ' ' + CLS_NEXT + ' ' + CLS_LAST + '"><a class="text">{next}</a></li>',
        PAGE: '<li class="' + CLS_PAGE + ' ' + CLS_NAV + '" title="{page}"><a class="text">{page}</a></li>',
        FIRST: '<li class="' + CLS_PAGE + ' ' + CLS_NAV + '" title="1"><a class="text">1</a></li>',
        LAST: '<li class="' + CLS_PAGE + ' ' + CLS_NAV + '" title="{total}"><a class="text">{total}</a></li>',
        ELLIPSIS: '<li class="' + CLS_PAGE + ' ' + CLS_ELLIPSIS + '"><span class="text">{ellipsis}</span></li>',
        CURRENT: '<li class="' + CLS_PAGE + ' ' + CLS_NAV + ' ' + CLS_CURRENT + '"><span class="text">{page}</span></li>'
    };

    Pagination.CUSTOMEVENTS = [
        'beforeChange',
        'afterChange'
    ];

    Pagination.prototype = {
        version: '0.1.3',
        constructor: Pagination,
        init: function (config) {

            this.set(config)._init().render().attachEvents();

            return this;
        },
        _init: function () {
            var self = this,
                beforeInit = this.get('beforeInit'),
                afterInit = this.get('afterInit');

            this.parent = $(this.get('parent'));

            if (isFunction(beforeInit)) {
                beforeInit(this._getEventData());
            }

            this.wrap = $(template({
                id: self.get('id') || guid('pagination')
            }, this.get('WRAP')));

            this.list = $(this.get('LIST'));

            this.totalPage = this.get('totalPage');
            this.currentPage = this.get('currentPage');
            this.pageSize = this.get('pageSize');

            if (isFunction(afterInit)) {
                afterInit(this._getEventData());
            }

            return this;
        },
        reload: function (config) {
            this.destroy().set(config)._init().render().attachEvents();

            return this;
        },
        set: function (config) {
            if ($.isPlainObject(config)) {
                $.extend(this.attributes, config);
            }

            return this;
        },
        get: function (prop) {
            return this.attributes[prop] || null;
        },
        _getEventData: function () {
            var self = this;

            return {
                pagination: self,
                totalPage: self.total(),
                currentPage: self.current(),
                pageSize: self.size()
            };
        },
        total: function (totalPage) {
            if (isNumber(totalPage)) {
                this.totalPage = totalPage;

                this.update();
            }
            else {
                return this.totalPage;
            }

            return this;
        },
        current: function (currentPage) {
            var totalPage = this.total();

            if (isNumber(currentPage)) {

                if (currentPage > totalPage) {
                    currentPage = totalPage;
                }

                this.currentPage = currentPage;

                this.update();
            }
            else {
                return this.currentPage;
            }

            return this;
        },
        size: function (pageSize) {
            var totalPage = this.total();

            if (isNumber(pageSize)) {

                if (pageSize > totalPage) {
                    pageSize = totalPage;
                }

                this.pageSize = pageSize;

                this.update();
            }
            else {
                return this.pageSize;
            }

            return this;
        },
        render: function () {
            var $parent = this.parent,
                $wrap = this.wrap,
                $list = this.list,
                beforeRender = this.get('beforeRender'),
                afterRender = this.get('afterRender');

            if (isFunction(beforeRender)) {
                beforeRender(this._getEventData());
            }

            $parent.empty().append($wrap.append($list));

            this.update();

            $wrap.height($list.find(DOT + CLS_PAGE)[0].offsetHeight);

            if (isFunction(afterRender)) {
                afterRender(this._getEventData());
            }

            return this;
        },
        show: function () {
            var $parent = this.parent;

            $parent.trigger('beforeShow', this._getEventData());

            $parent.removeClass(CLS_INVISIBLE);

            $parent.trigger('afterShow', this._getEventData());

            return this;
        },
        hide: function () {
            var $parent = this.parent;

            $parent.trigger('beforeHide', this._getEventData());

            $parent.addClass(CLS_INVISIBLE);

            $parent.trigger('afterHide', this._getEventData());

            return this;
        },
        isDisabled: function () {
            return this.disabled;
        },
        disable: function () {
            var $parent = this.parent;

            $parent.trigger('beforeDisable', this._getEventData());

            this.disabled = true;
            $parent.disabled = true;
            $parent.addClass(CLS_DISABLED);

            $parent.trigger('afterDisable', this._getEventData());

            return this;
        },
        enable: function () {
            var $parent = this.parent;

            $parent.trigger('beforeEnable', this._getEventData());

            this.disabled = false;
            $parent.disabled = false;
            $parent.removeClass(CLS_DISABLED);

            $parent.trigger('afterEnable', this._getEventData());

            return this;
        },
        destroy: function () {
            var $parent = this.parent;

            $parent.trigger('beforeDestroy', this._getEventData());

            $parent.off().empty();

            $parent.trigger('afterDestroy', this._getEventData());

            return this;
        },
        update: function () {
            var self = this,
                totalPage = this.total(),
                currentPage = this.current(),
                pageSize = this.size(),
                pageRange = pageSize % 2 === 0 ? (pageSize / 2) : ((pageSize - 1) / 2),
                startRange = 0,
                endRange = pageSize % 2 === 0 ? currentPage + pageRange - 1 : currentPage + pageRange,
                startEllipsisSize = pageRange + 1,
                showPages = this.get('showPages'),
                showPrev = this.get('showPrev'),
                showNext = this.get('showNext'),
                showPageNumbers = this.get('showPageNumbers'),
                $list = this.list,
                $pages = $(template({
                    current: currentPage,
                    total: totalPage
                }, this.get('PAGES'))),
                $first = $(this.get('FIRST')),
                $prev = $(template({
                    prev: self.get('prevText')
                }, this.get('PREV'))),
                $next = $(template({
                    next: self.get('nextText')
                }, this.get('NEXT'))),
                $last = $(template({
                    total: totalPage
                }, this.get('LAST'))),
                ellipsisHTML = this.get('ELLIPSIS'),
                add = function (page) {
                    var current = self.current(),
                        beforeDrawPage = self.get('beforeDrawPage'),
                        afterDrawPage = self.get('afterDrawPage'),
                        $page,
                        args;

                    $page = $(template({
                        page: page
                    }, self.get('PAGE')));

                    if (page === current) {
                        $page = $(template({
                            page: page
                        }, self.get('CURRENT')));
                    }

                    args = {
                        page: page,
                        pageEl: $page
                    };

                    if (isFunction(beforeDrawPage)) {
                        beforeDrawPage($.extend(self._getEventData(), args));
                    }

                    if (!showPages && !showPrev && page === 1) {
                        $page.addClass(CLS_FIRST);
                    }

                    if (!showNext && page === totalPage) {
                        $page.addClass(CLS_LAST);
                    }

                    self.list.append($page);

                    if (isFunction(afterDrawPage)) {
                        afterDrawPage($.extend(self._getEventData(), args));
                    }
                };

            if (totalPage < 1) {
                return this;
            }

            // 先隐藏页面容器列表，然后在隐藏状态下绘制DOM，性能更好
            $list.addClass(CLS_HIDDEN).empty();

            // 页码索引信息
            if (showPages) {
                $list.append($pages);
            }
            else {
                $prev.addClass(CLS_FIRST);
            }

            // 上一页
            if (showPrev) {
                $list.append($prev);
            }

            // 显示数字页码
            if (showPageNumbers) {

                // 总页数大于要显示的页码个数
                if (totalPage > pageSize) {

                    // 当前页数大于最小的显示占位符页数，则开始显示...占位符
                    if (currentPage > startEllipsisSize) {

                        // 计算数字页码的起始页数
                        startRange = currentPage - startEllipsisSize;

                        // 当前页数离最后一页小于pageSize的范围
                        // 起始页码就是从最有一页开始，倒推pageSize个
                        if (totalPage - currentPage <= pageRange) {
                            startRange = totalPage - (pageSize + 1);
                        }
                        else {

                            // 起始页码最小值为0
                            if (startRange <= 0) {
                                startRange = 0;
                            }

                        }

                        // 起页码大于2时，显示第一页
                        if (startRange >= 1) {
                            if (!showPages && !showPrev) {
                                $first.addClass(CLS_FIRST);
                            }

                            $list.append($first);
                        }

                        // 起始页码大于3时，显示第一个占位符
                        if (startRange >= 2) {
                            $list.append($(template({
                                ellipsis: self.get('ellipsisText')
                            }, ellipsisHTML)));
                        }

                        // 计算pageSize范围类的截至页码
                        if (endRange >= totalPage - 1) {
                            endRange = totalPage - 1;
                        }
                    }
                    else {

                        endRange = pageSize + 1;

                        if (endRange >= totalPage - 1) {
                            endRange = totalPage - 1;
                        }
                    }

                    // 显示数字页面
                    for (; startRange < endRange; startRange++) {
                        add(startRange + 1);
                    }

                    // 显示第二个...占位符号
                    if (endRange <= totalPage - 2) {
                        $list.append($(template({
                            ellipsis: self.get('ellipsisText')
                        }, ellipsisHTML)));
                    }

                    if (!showNext) {
                        $last.addClass(CLS_LAST);
                    }

                    // 选中了最后一页
                    if (totalPage === currentPage) {
                        $last.addClass(CLS_CURRENT);
                    }

                    $list.append($last);
                }
                else {

                    // 总页数小于设置的pageSize，则直接显示所有的页码
                    for (; startRange < totalPage; startRange++) {
                        add(startRange + 1);
                    }
                }

            }

            // Next page button
            if (showNext) {
                $list.append($next);
            }

            $list.removeClass(CLS_HIDDEN);

            this.disableButtons().align(this.get('align'));

            return this;
        },
        align: function (pageAlign) {
            var $list = this.list;

            this.set({
                align: pageAlign
            });

            switch (pageAlign) {
                case 'left':

                    if ($list.hasClass(CLS_CENTER)) {
                        $list.removeClass(CLS_CENTER);
                    }

                    if ($list.hasClass(CLS_RIGHT)) {
                        $list.removeClass(CLS_RIGHT);
                    }

                    break;
                case 'center':

                    if ($list.hasClass(CLS_RIGHT)) {
                        $list.removeClass(CLS_RIGHT);
                    }

                    $list.addClass(CLS_CENTER).css('margin-left', '-' + ($list.width() / 2) + 'px');

                    break;
                case 'right':

                    if ($list.hasClass(CLS_CENTER)) {
                        $list.removeClass(CLS_CENTER);
                    }

                    $list.addClass(CLS_RIGHT);

                    break;
            }

            return this;
        },
        disableButtons: function () {
            var total = this.total(),
                current = this.current(),
                alwaysShowPrevNext = this.get('alwaysShowPrevNext'),
                disableClassName = CLS_INVISIBLE,
                $list = this.list,
                $prev = $list.find(DOT + CLS_PREV).eq(0),
                $next = $list.find(DOT + CLS_NEXT).eq(0);

            if (current === 1) {

                if (alwaysShowPrevNext) {
                    disableClassName = CLS_DISABLED;
                }

                $prev.addClass(disableClassName);
                $next.removeClass(disableClassName);
            }
            else {
                if (current > 1 && current < total) {

                    if (alwaysShowPrevNext) {
                        disableClassName = CLS_DISABLED;
                    }

                    $prev.removeClass(disableClassName);
                    $next.removeClass(disableClassName);
                }
                else {

                    if (current === total) {

                        if (alwaysShowPrevNext) {
                            disableClassName = CLS_DISABLED;
                        }

                        $prev.removeClass(disableClassName);
                        $next.addClass(disableClassName);
                    }

                }
            }

            return this;
        },
        prev: function () {
            var $parent = this.parent;

            $parent.trigger('beforeChange', this._getEventData());

            this.currentPage -= 1;

            if (this.currentPage <= 1) {
                this.currentPage = 1;
            }

            this.update();

            $parent.trigger('afterChange', this._getEventData());

            return this;
        },
        change: function (page) {
            var $parent = this.parent;

            $parent.trigger('beforeChange', this._getEventData());

            this.currentPage = page;

            this.update();

            $parent.trigger('afterChange', this._getEventData());

            return this;
        },
        next: function () {
            var $parent = this.parent,
                total = this.total();

            $parent.trigger('beforeChange', this._getEventData());

            this.currentPage += 1;

            this.update();

            if (this.currentPage >= total) {
                this.currentPage = total;
            }

            $parent.trigger('afterChange', this._getEventData());

            return this;
        },
        on: function (evtName, callback) {

            if ($.inArray(Pagination.CUSTOMEVENTS, evtName) && $.isFunction(callback)) {
                this.parent.on(evtName, function (evt, args) {
                    callback(args);

                    evt.stopPropagation();
                    evt.preventDefault();
                });
            }

            return this;
        },
        trigger: function (evtName) {

            if ($.inArray(Pagination.CUSTOMEVENTS, evtName)) {
                this.parent.trigger(evtName);
            }

            return this;
        },
        off: function (evtName) {
            this.parent.off(evtName);

            return this;
        },
        attachEvents: function () {
            var self = this,
                $parent = this.parent,
                EventData = {
                    context: self
                };

            // 点击上一页按钮
            $parent.delegate(DOT + CLS_PREV, 'click', EventData, this._onPrevClick);

            // 点击页码按钮
            $parent.delegate(DOT + CLS_NAV, 'click', EventData, this._onPageClick);

            // 点击下一页按钮
            $parent.delegate(DOT + CLS_NEXT, 'click', EventData, this._onNextClick);

            return this;
        },
        _onPrevClick: function (evt) {
            var pagination = evt.data.context;

            if (pagination.isDisabled() || $(this).hasClass(CLS_DISABLED)) {
                return pagination;
            }

            pagination.prev();

            evt.stopPropagation();
            evt.preventDefault();

            return pagination;
        },
        _onPageClick: function (evt) {
            var pagination = evt.data.context,
                $page = $(this);

            if (pagination.isDisabled() || $page.hasClass(CLS_CURRENT) || $page.hasClass(CLS_DISABLED)) {
                return pagination;
            }

            pagination.change(parseInt($page.attr('title'), 10));

            evt.stopPropagation();
            evt.preventDefault();

            return pagination;
        },
        _onNextClick: function (evt) {
            var pagination = evt.data.context;

            if (pagination.isDisabled() || $(this).hasClass(CLS_DISABLED)) {
                return pagination;
            }

            pagination.next();

            evt.stopPropagation();
            evt.preventDefault();

            return pagination;
        }
    };

    if (!noGlobal) {
        window.Pagination = Pagination;
    }

    if (!$.fn.pagination) {
        $.fn.extend({
            pagination: function (config) {
                config.parent = $(this);

                return new Pagination(config);
            }
        });
    }

    return Pagination;
}));