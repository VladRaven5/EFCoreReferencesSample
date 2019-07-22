using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreReferencesSample.DAL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EFCoreReferencesSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = InitDb())
            {
                var pet = await context.Pets
                    .Include(p => p.Owner)
                    .FirstOrDefaultAsync(p => p.Id == 1);

                var persons = await context.Persons.ToListAsync();

                //set default owner to repeatability
                await ResetPet(pet, context);

                await CheckReferencePropertyChanged(persons, pet, context);

                await ResetPet(pet, context);

                await CheckIdPropertyChanged(persons, pet, context);

                await ResetPet(pet, context);

                await CheckBothChanged(persons, pet, context);

                await ResetPet(pet, context);

                await SetOneAfterAnother(persons, pet, context);
            }

            //Console.ReadKey();
        }

        private static async Task SetOneAfterAnother(List<Person> persons, Pet pet, MainContext context)
        {
            var firstPerson = persons.First();
            var secondPerson = persons[1];

            pet.Owner = firstPerson;
            await context.SaveChangesAsync();

            PrintData($"Pet after setting Owner = {firstPerson.Name} but before OwnerId of {secondPerson.Name} ({secondPerson.Id})", pet);

            pet.OwnerId = secondPerson.Id;

            PrintData(
                $"Pet after setting Owner = {firstPerson.Name} and after OwnerId of {secondPerson.Name} ({secondPerson.Id}), but before saving changes",
                pet);

            await context.SaveChangesAsync();

            PrintData(
                $"Pet after setting Owner = {firstPerson.Name} and after OwnerId of {secondPerson.Name} ({secondPerson.Id}), and after saving changes",
                pet);
            
        }

        private static async Task ResetPet(Pet pet, MainContext context)
        {
            pet.OwnerId = null;
            await context.SaveChangesAsync();
            PrintData("Pet after reset", pet);
        }

        private static async Task CheckReferencePropertyChanged(List<Person> persons, Pet pet, MainContext context)
        {
            var firstPerson = persons.First();

            pet.Owner = firstPerson;

            PrintData($"Pet after set Owner = {firstPerson.Name}, but before save changes", pet);

            await context.SaveChangesAsync();

            PrintData($"Pet after set Owner = {firstPerson.Name}, after save changes", pet);
        }

        private static async Task CheckIdPropertyChanged(List<Person> persons, Pet pet, MainContext context)
        {
            var secondPerson = persons[1];

            pet.OwnerId = secondPerson.Id;

            PrintData($"Pet after set OwnerId to {secondPerson.Name} ({secondPerson.Id}), but before save changes", pet);

            await context.SaveChangesAsync();

            PrintData($"Pet after set Owner = {secondPerson.Name} ({secondPerson.Id}), after save changes", pet);
        }

        private static async Task CheckBothChanged(List<Person> persons, Pet pet, MainContext context)
        {
            var firstPerson = persons.First();
            var secondPerson = persons[1];

            pet.OwnerId = firstPerson.Id;
            pet.Owner = secondPerson;

            PrintData($"Pet after set OwnerId to {firstPerson.Name} ({firstPerson.Id}) and Owner = {secondPerson.Name}, but before save changes", pet);

            await context.SaveChangesAsync();

            PrintData($"Pet after set OwnerId to {firstPerson.Name} ({firstPerson.Id}) and Owner = {secondPerson.Name}, after save changes", pet);

            await ResetPet(pet, context);

            pet.Owner = firstPerson;
            pet.OwnerId = secondPerson.Id;

            PrintData($"Pet after set Owner = {firstPerson.Name} and OwnerId to {secondPerson.Name} ({secondPerson.Id}), but before save changes", pet);

            await context.SaveChangesAsync();

            PrintData($"Pet after set Owner = {firstPerson.Name} and OwnerId to {secondPerson.Name} ({secondPerson.Id}), after save changes", pet);
        }



        private static MainContext InitDb()
        {
            var context = new MainContext();
            context.Database.Migrate();
            return context;
        }

        private static void PrintData(string message, object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            Console.WriteLine($"{message}:\n{serializedData}\n");
        }
    }
}
