using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulacro.Data;
using Simulacro.Models;



namespace Simulacro.Controllers{


    public class CompaniesController: Controller{

        public readonly CompContext _context;

        public CompaniesController(CompContext context){

            _context= context;
        }

        //vista Index
      

        public async Task<IActionResult> Index(string SearchString){

            var comp = from find in _context.Companies select find;

            if (!string.IsNullOrEmpty(SearchString)){
                comp= comp.Where(c => c.Name.Contains(SearchString));
            }

            return View(await comp.ToListAsync());
        }

        //Detalles

        public async Task<IActionResult> Details(int? id){

            return View(await _context.Companies.FirstOrDefaultAsync(m => m.Id == id));
        }

        //Crear
        public  IActionResult Create(){
            return View();
        }

        public IActionResult CreateCompanie( Companie comp){

            _context.Companies.Add(comp);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }

        // Editar

        public  async Task<IActionResult> Edit(int? id){
            return View(await _context.Companies.FirstOrDefaultAsync(m => m.Id ==id));
        }

        public IActionResult EditCompanie( Companie comp){

            _context.Companies.Update(comp);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }

        //Eliminar

         public  async Task<IActionResult> Delete(int? Id){

            var companie= await _context.Companies.FindAsync(Id);
             _context.Companies.Remove(companie);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        

      


        
    }


}