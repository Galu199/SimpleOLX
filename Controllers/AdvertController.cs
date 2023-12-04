using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SimpleOLX.Controllers
{
	public class AdvertController : Controller
	{
		// GET: AdvertController
		public ActionResult Index()
		{
			return Ok();
		}

		// GET: AdvertController/Details/5
		public ActionResult Details(int id)
		{
			return Ok();
		}

		// GET: AdvertController/Create
		public ActionResult Create()
		{
			return Ok();
		}

		// POST: AdvertController/Create
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
				return Ok();
			}
		}

		// GET: AdvertController/Edit/5
		public ActionResult Edit(int id)
		{
			return Ok();
		}

		// POST: AdvertController/Edit/5
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
				return Ok();
			}
		}

		// GET: AdvertController/Delete/5
		public ActionResult Delete(int id)
		{
			return Ok();
		}

		// POST: AdvertController/Delete/5
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
				return Ok();
			}
		}
	}
}
