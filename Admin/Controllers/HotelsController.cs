using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Hotels;
using Infrastructure;


namespace WebApp.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get
        #region نمایش لیست هتل
        // لیست هتل‌ها
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }
        #endregion

        //Get
        #region لود صفحه ایجاد
        // صفحه ایجاد هتل
        public IActionResult Create()
        {
            return PartialView();
        }

        #endregion

        //Post
        #region ایجاد هتل
        // عملیات ایجاد هتل
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
          
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(hotel);
        }
        #endregion

        //Get
        #region لود صفحه ویرایش
        // صفحه ویرایش
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound();

            return View(hotel);
        }

        #endregion

        //Post
        #region عملیات ویرایش
        // عملیات ویرایش
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Hotel hotel)
        //{
        //    if (id != hotel.Id)
        //        return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(hotel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!HotelExists(hotel.Id))
        //                return NotFound();
        //            else
        //                throw;
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return PartialView(hotel);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hotel hotel)
        {
            if (id != hotel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
                        return NotFound();
                    else
                        throw;
                }

                // اگر درخواست AJAX بود، وضعیت موفقیت برگردون
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true });
                }

                // در حالت عادی:
                return RedirectToAction(nameof(Index));
            }

            // اگر مدل معتبر نبود و از طریق AJAX اومده
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Edit", hotel);
            }

            // در غیر این صورت
            return View(hotel);
        }

        #endregion

        //Get
        #region صفحه جزئیات
        // صفحه جزئیات
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
                return NotFound();

            return PartialView(hotel);
        }
        #endregion

        //Get
        #region نمایش حذف
        // صفحه حذف
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
                return NotFound();

            return PartialView(hotel);
        }
        #endregion

        //Post
        #region عملیات حذف
        // عملیات حذف
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}