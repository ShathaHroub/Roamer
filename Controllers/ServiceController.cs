using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roamer.Data;
using Roamer.Models;
using Roamer.ViewModels;

namespace Roamer.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {

        private AppDbContext db;
        private IWebHostEnvironment WebHostEnvironment;
        public ServiceController(AppDbContext _db, IWebHostEnvironment webHostEnvironment)
        {

            db = _db;
            WebHostEnvironment = webHostEnvironment;
        }


        #region Places


        public IActionResult Place()
        {

            var places = db.Places.ToList();
            return View(places);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePlace()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePlace(PlaceViewModel model)
        {

            string stringFileName = UploadFile(model);
            var place = new Place
            {
                PlaceName = model.PlaceName,
                PlaceHeading1 = model.PlaceHeading1,
                PlaceHeading2 = model.PlaceHeading2,
                PlaceContent1 = model.PlaceContent1,
                PlaceContent2 = model.PlaceContent2,
                PlaceContent3 = model.PlaceContent3,
                PlaceDescription = model.PlaceDescription,
                PlaceImage = stringFileName
            };
            db.Places.Add(place);
            db.SaveChanges();
            return RedirectToAction(nameof(Place));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditPlace(int id)
        {
            var place = db.Places.Find(id);
            return View(place);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditPlace(int id, PlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var place = db.Places.Find(id);
                string stringfile = UploadFile(model);
                place.PlaceName = model.PlaceName;
                place.PlaceHeading1 = model.PlaceHeading1;
                place.PlaceHeading2 = model.PlaceHeading2;
                place.PlaceContent1 = model.PlaceContent1;
                place.PlaceContent2 = model.PlaceContent2;
                place.PlaceContent3 = model.PlaceContent3;
                place.PlaceDescription = model.PlaceDescription;
                place.PlaceImage = stringfile;
                db.SaveChanges();
                return RedirectToAction("Place");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeletePlace(int id)
        {
            var place = db.Places.Find(id);
            return View(place);
        }

        [HttpPost, ActionName("DeletePlace")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePlacePost(int id)
        {
            var place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
            return RedirectToAction("Place");

        }


        public IActionResult PlaceDetail(int id)
        {
            var place = db.Places.FirstOrDefault(x => x.PlaceId == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);

        }

        private string UploadFile(PlaceViewModel model)
        {
            string fileName = null;
            if (model.PlaceImage != null)
            {
                string UploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Image");
                fileName = Guid.NewGuid().ToString() + "-" + model.PlaceImage.FileName;
                string FilePath = Path.Combine(UploadDir, fileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.PlaceImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }






        #endregion

        #region Guides

        [AllowAnonymous]
        public IActionResult Guides()
        {
            return View(db.Guides.ToList());
        }

        [Authorize(Roles = "Admin")]

        public IActionResult CreateGuide()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult CreateGuide(GuideViewModel model)
        {
            string stringfile = UploadFileGuide(model);
            var guide = new Guide
            {
                GuideName = model.GuideName,
                GuideDescription = model.GuideDescription,
                GuideUrl = stringfile
            };
            db.Guides.Add(guide);
            db.SaveChanges();
            return RedirectToAction("Guides", "Service");
        }
        private string UploadFileGuide(GuideViewModel model)
        {

            string fileName = null;
            if (model.GuideUrl != null)
            {
                string UploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Image");
                fileName = Guid.NewGuid().ToString() + "-" + model.GuideUrl.FileName;
                string FilePath = Path.Combine(UploadDir, fileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.GuideUrl.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [Authorize(Roles = "Admin")]

        public IActionResult EditGuide(int id)
        {
            var guide = db.Guides.Find(id);
            return View(guide);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditGuide(int id, GuideViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guide = db.Guides.Find(id);
                string stringfile = UploadFileGuide(model);
                guide.GuideName = model.GuideName;
                guide.GuideDescription = model.GuideDescription;
                guide.GuideUrl = stringfile;
                db.SaveChanges();
                return RedirectToAction("Guides");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteGuide(int id)
        {
            var guide = db.Guides.Find(id);
            return View(guide);
        }

        [HttpPost, ActionName("DeleteGuide")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteGuidePost(int id)
        {
            var guide = db.Guides.Find(id);
            db.Guides.Remove(guide);
            db.SaveChanges();
            return RedirectToAction("Guides");

        }

        #endregion

        #region SignalR
        [Authorize]
        public IActionResult ChatRoom()
        {
            return View();
        }

        #endregion


        #region Blogs

        private string UploadFilePost(PostViewModel model)
        {

            string fileName = null;
            if (model.PostImagePath != null)
            {
                string UploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Image");
                fileName = Guid.NewGuid().ToString() + "-" + model.PostImagePath.FileName;
                string FilePath = Path.Combine(UploadDir, fileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.PostImagePath.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Posts()
        {
            return View(await db.Posts.ToListAsync());
        }

        [HttpPost]
        public IActionResult Posts(string title)
        {

            return View(db.Posts.Where(x => x.PostTitle.Contains(title)));
        }
        [AllowAnonymous]
        public async Task<IActionResult> PostDetail(int id)
        {
            var post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);

        }

        [Authorize(Roles = "Expert")]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Expert")]

        public async Task<IActionResult> CreatePost(PostViewModel model)
        {


            string stringfile = UploadFilePost(model);
            var post = new Post
            {
                PostTitle = model.PostTitle,
                PostContent = model.PostContent,
                PostHeading = model.PostHeading,
                PostContent2 = model.PostContent2,
                PostHeading2 = model.PostHeading2,
                PostContent3 = model.PostContent3,
                CreatedAt = DateTime.Now,
                PostImagePath = stringfile

            };

            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Posts));



        }


        [HttpGet]
        [Authorize(Roles = "Expert")]
        public async Task<IActionResult> EditPost(int id)
        {
            var post = await db.Posts.FindAsync(id);

            return View(post);
        }

        [HttpPost]
        [Authorize(Roles = "Expert")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditPost(int id, PostViewModel model)
        {

            if (ModelState.IsValid)
            {

                var post = await db.Posts.FindAsync(id);
                string stringfile = UploadFilePost(model);
                if (post == null)
                {
                    return NotFound();
                }


                post.PostTitle = model.PostTitle;
                post.PostContent = model.PostContent;
                post.PostHeading = model.PostHeading;
                post.PostContent2 = model.PostContent2;
                post.PostHeading2 = model.PostHeading2;
                post.PostContent3 = model.PostContent3;
                post.CreatedAt = DateTime.Now;
                post.PostImagePath = stringfile;


                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Posts));


            }

            return View(model);
        }


        [Authorize(Roles = "Expert")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await db.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("DeletePost")]
        [Authorize(Roles = "Expert")]
        public async Task<IActionResult> DeletePostConfirmed(int id)
        {
            var post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Posts));
        }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken] 
        //    public async Task<IActionResult> Create([Bind("Name,Content,PostId")] Comment comment)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // Set the comment's creation time and user
        //            comment.CreatedAt = DateTime.Now;
        //          // Assuming you use Identity for authentication

        //            // Add the comment to the database
        //            db.Add(comment);
        //            await db.SaveChangesAsync();

        //            // Redirect to the post or wherever you want after a successful comment creation
        //            return RedirectToAction("PostDetail", new { id = comment.PostId });
        //        }

        //        // If the model state is not valid, return to the comment creation form with errors
        //        return View(comment);
        //    }
        //}
        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateComment(int postId,string name, string message)
        {
            var comment = new Comment
            {
                Name = name,
                Content = message,
                PostId = postId,
            };
            db.Comments.Add(comment);
            return RedirectToAction("PostDetail" , new {id = postId});
        }
        //public IActionResult CreateComments(int id , string name , string message)
        //{

        //    return View();
        //}

        #endregion

    }
}

        