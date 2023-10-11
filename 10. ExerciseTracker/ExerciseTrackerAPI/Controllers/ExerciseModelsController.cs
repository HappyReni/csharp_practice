﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExerciseTrackerAPI.Model;

namespace ExerciseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseModelsController : ControllerBase
    {
        private readonly ExerciseContext _context;

        public ExerciseModelsController(ExerciseContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseModel>>> GetExercises()
        {
          if (_context.Exercises == null)
          {
              return NotFound();
          }
            return await _context.Exercises.ToListAsync();
        }

        // GET: api/ExerciseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseModel>> GetExerciseModel(int id)
        {
          if (_context.Exercises == null)
          {
              return NotFound();
          }
            var exerciseModel = await _context.Exercises.FindAsync(id);

            if (exerciseModel == null)
            {
                return NotFound();
            }

            return exerciseModel;
        }

        // PUT: api/ExerciseModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExerciseModel(int id, ExerciseModel exerciseModel)
        {
            if (id != exerciseModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(exerciseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExerciseModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExerciseModel>> PostExerciseModel(ExerciseModel exerciseModel)
        {
          if (_context.Exercises == null)
          {
              return Problem("Entity set 'ExerciseContext.Exercises'  is null.");
          }
            _context.Exercises.Add(exerciseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExerciseModel", new { id = exerciseModel.Id }, exerciseModel);
        }

        // DELETE: api/ExerciseModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseModel(int id)
        {
            if (_context.Exercises == null)
            {
                return NotFound();
            }
            var exerciseModel = await _context.Exercises.FindAsync(id);
            if (exerciseModel == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exerciseModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseModelExists(int id)
        {
            return (_context.Exercises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
