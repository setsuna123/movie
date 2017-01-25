using MovieDetails.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MovieDetails.Controllers
{
    public class HomeController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(newMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(newMovie);
            }
        }

       
        public ActionResult Index()
        {
           
            return View(db.Movies.ToList());
         
        }

        public ActionResult Delete(int id)
        {
            var movie = db.Movies.FirstOrDefault(o => o.Id == id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Delete(int id, bool confirm)
        {
            var movie = db.Movies.FirstOrDefault(o => o.Id == id);

            db.Movies.Remove(movie);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id=0)
        {
            Movie mov = db.Movies.Find(id);
            if (mov == null)
            {
                return HttpNotFound();
            }
            return View(mov);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Director,DateReleased")] Movie mov)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mov).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mov);
        }

    }

}