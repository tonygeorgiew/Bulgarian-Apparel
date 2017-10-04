using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bulgarian_Apparel.Services;
using Bulgarian_Apparel.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IMapper mapper;

        public HomeController(IPostsService postsService, IMapper mapper)
        {
            this.postsService = postsService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var posts = this.postsService
                .GetAll()
                .ProjectTo<PostViewModel>()
                .ToList();

           
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult View1()
        {
            return View();
        }
    }
}