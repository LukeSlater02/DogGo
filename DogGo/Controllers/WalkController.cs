using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;

namespace DogGo.Controllers
{
    public class WalkController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;

        public WalkController(IDogRepository dogRepo, IWalkerRepository walkerRepo)
        {
            _dogRepo = dogRepo;
            _walkerRepo = walkerRepo;
        }


        // GET: WalkController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WalkController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalkController/Create
        public ActionResult Create()
        {
            CreateWalkViewModel vm = new()
            {
                WalkerList = _walkerRepo.GetAllWalkers(),
                Walk = new Walk(),
                DogList = _dogRepo.GetAllDogs()
            };
            return View(vm);
        }

        // POST: WalkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
