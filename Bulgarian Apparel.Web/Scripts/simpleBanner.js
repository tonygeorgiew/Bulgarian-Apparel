(function($,w)
{

	function repeat(target, n)
	{
		return (new Array(n + 1)).join(target);
	}

	$.fn.simpleBanner=function(cfg)
	{
		var options=
		{
			dots:null,
			speed:500,
			easing:'swing',
			arrows:false,
			autoPlay:true,
			autoPlayDuration:5000,
			eventType:'click',
			animation:null,
			onSwitch:$.noop,
			resizeHeight:0 // 默认不修改高度
		};
		cfg=$.extend(options,cfg);
		var $ul=this.find('ul');
		var $li=$ul.find('li');
		var $img=$li.find('img');
		var len=$li.length;
		var width=len+'00%';
		var perWidth=(100/len).toFixed(2)+'%';

		this.css({overflow:'hidden',width:'100%',position:'relative'});
		$ul.css({width:width,overflow:'hidden',margin:0,zoom:1});
		$li.css({width:perWidth,height:'100%',float:'left',display:'block'});
		$img.css({width:'100%',height:'100%',display:'block'});

		if(cfg.resizeHeight==1) // 以最大高度固定，小高度图片可能会变形
		{
			$ul.css({height:$ul.height()});
			$(window).resize(function(){
				$ul.css({height:'auto'});
			    setTimeout(function()
			    {
					$ul.css({height:$ul.height()});
			    },0);
			});
		}
		if(cfg.dots)
		{
			this.append('<ol class="'+cfg.dots+'"><li class="active"></li>'+repeat('<li></li>',len-1)+'</ol>');
			var $ol=this.find('.'+cfg.dots);

			$ol.on(cfg.eventType,'li',function()
			{
				var current=$(this).index();
				if(index!=current)
				{
					play(current);
					index=current;
				}
			});
		}

		var timer,index=len*1e5;


		function next()
		{
			var current=++index%len;
			play(current);
		}

		function prev()
		{
			var current=--index%len;
			play(current);
		}

		function autoplay()
		{
			if(!timer)
			{
				timer=setInterval(next,cfg.autoPlayDuration);
			}
		}
		function stopplay()
		{
			clearInterval(timer);
			timer=null;
		}

		function play(current)
		{
			if(cfg.resizeHeight==2) // 自适应内容高度，图片大小不一整体高度会切换。
			{
				$ul.css('height','auto');
				var h = $ul.find('li').eq(current).height();
				$ul.css({'height':h});
			}
			animation.play(current);
			if(cfg.dots)
			{
				$ol.find('li').eq(current).addClass('active').siblings().removeClass('active');
			}
			cfg.onSwitch($ul,current);
		}
		var defaultAnimate=function(current)
		{
			$ul.css({'margin-left':'-'+current+'00%'}).data('active',current);
		};
		var animation=
		{
			fade:function(current)
			{
				$ul.find('li').eq(current).find('img').hide().fadeIn(cfg.speed);
				$ul.css({'margin-left':'-'+current+'00%'}).data('active',current);
			},
			slide:function(current)
			{
				$ul.animate({'margin-left':'-'+current+'00%'},cfg.speed,cfg.easing).data('active',current);
			},
			change:defaultAnimate
		};
		animation.play=animation[options.animation]?animation[options.animation]:defaultAnimate;



		if(cfg.autoPlay)
		{
			this.hover(function()
			{
				stopplay();
			},function()
			{
				autoplay();
			});
			autoplay();
		}

		return {
			play:play,
			next:next,
			prev:prev,
			pause:stopplay,
			unpause:autoplay,
		};


	};


})(jQuery,window);
