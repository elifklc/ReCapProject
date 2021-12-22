using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            //BrandTest();
            //CarTest();
            IUserService userService = new UserManager(new EfUserDal());
            ICustomerService customerService = new CustomerManager(new EfCustomerDal());
            
            //foreach (var user in userService.GetAll().Data)
            //{
            //    Console.WriteLine(user.FirstName + " " + user.LastName);
            //}
            //userService.Add(new User { Id = 4, Email = "test@gmail.com", FirstName = "Ahmet", LastName = "Külcü", Password = "123456" });
            //customerService.Add(new Customer { UserId =4, CompanyName = "Volkswagen" });

            //foreach (var user in userService.GetAll().Data)
            //{
            //    Console.WriteLine(user.FirstName + " " + user.LastName);
            //}
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            var result = carManager.GetCarDetails();

            if (result.Success == true)
            {
                foreach (var car in carManager.GetCarDetails().Data)
                {
                    Console.WriteLine("***************");
                    //Console.WriteLine(car.CarName + " " + car.BrandName);
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }
        }


        //private static void BrandTest()
        //{
        //    BrandManager brandManager = new BrandManager(new EfBrandDal());
        //    foreach (var brand in brandManager.GetAll())
        //    {
        //        Console.WriteLine(brand.BrandName);
        //    }
        //}

        //    private static void Test()
        //    {
        //        CarManager carManager = new CarManager(new EfCarDal());

        //        foreach (var car in carManager.GetCarsByBrandId(1))
        //        {
        //            Console.WriteLine(car.Description);
        //        }

        //        //carManager.Add(new Car {BrandId = 1 , ColorId = 2, DailyPrice = 0, Description="xxxxx", ModelYear = 2000});

        //        foreach (var car in carManager.GetAll())
        //        {
        //            Console.WriteLine(car.Description);
        //        }

        //        BrandManager brandManager = new BrandManager(new EfBrandDal());

        //        //brandManager.Add(new Brand {BrandName= "X" });

        //        foreach (var brand in brandManager.GetAll())
        //        {
        //            Console.WriteLine(brand.BrandName);
        //        }
        //    }
        //}
    }
}

