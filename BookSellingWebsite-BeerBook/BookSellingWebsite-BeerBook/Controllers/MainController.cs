using BookSellingWebsite_BeerBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSellingWebsite_BeerBook.Controllers
{
    public class MainController : Controller
    {
        public static List<Book_> getAllBooks()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var books = (from u in _context.Book_.AsEnumerable()
                             select new
                             {
                                 BookID = u.BookID_,
                                 Name = u.Name_,
                                 Quantity = u.Quantity_,
                                 Price = u.Price_,
                                 Description = u.Description_,
                                 AuthorID = u.AuthorID_,
                                 BhID = u.BhID_,
                                 Image = u.Image_,
                                 Language = u.Language_,
                                 BookHouse = u.BookHouse_
                             })
                            .Select(x => new Book_
                            {
                                BookID_ = x.BookID,
                                Name_ = x.Name,
                                Quantity_ = x.Quantity,
                                Price_ = x.Price,
                                Description_ = x.Description,
                                AuthorID_ = x.AuthorID,
                                BhID_ = x.BhID,
                                Image_ = x.Image,
                                Language_ = x.Language,
                                BookHouse_ = x.BookHouse
                            }).ToList();
                return books;
            }
        }
        public static Book_ getABook(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var book = (from u in (from i in _context.Book_.AsEnumerable()
                                       select i)
                            where u.BookID_ == id
                            select new
                            {
                                BookID = u.BookID_,
                                Name = u.Name_,
                                Quantity = u.Quantity_,
                                Price = u.Price_,
                                Description = u.Description_,
                                AuthorID = u.AuthorID_,
                                BhID = u.BhID_,
                                Image = u.Image_,
                                Language = u.Language_,
                                BookHouse = u.BookHouse_
                            })
                            .Select(x => new Book_
                            {
                                BookID_ = x.BookID,
                                Name_ = x.Name,
                                Quantity_ = x.Quantity,
                                Price_ = x.Price,
                                Description_ = x.Description,
                                AuthorID_ = x.AuthorID,
                                BhID_ = x.BhID,
                                Image_ = x.Image,
                                Language_ = x.Language,
                                BookHouse_ = x.BookHouse
                            }).SingleOrDefault();
                return book;
            }
        }
        public static List<Author_> getAllAuthor()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var authors = (from u in _context.Author_.AsEnumerable()
                               select new
                               {
                                   AuthorID = u.AuthorID_,
                                   Name = u.Name_,
                                   Nationality = u.Nationality_,
                                   BirthDate = u.BirthDate_,
                                   Sex = u.Sex_,
                                   Description = u.Description_,
                               })
                            .Select(x => new Author_
                            {
                                AuthorID_ = x.AuthorID,
                                Name_ = x.Name,
                                Nationality_ = x.Nationality,
                                BirthDate_ = x.BirthDate,
                                Sex_ = x.Sex,
                                Description_ = x.Description,
                            }).ToList();
                return authors;
            }
        }
        public static Author_ getAAuthor(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var author = (from u in (from i in _context.Author_.AsEnumerable()
                                       select i)
                            where u.AuthorID_ == id
                              select new
                              {
                                  AuthorID = u.AuthorID_,
                                  Name = u.Name_,
                                  Nationality = u.Nationality_,
                                  BirthDate = u.BirthDate_,
                                  Sex = u.Sex_,
                                  Description = u.Description_,
                              })
                            .Select(x => new Author_
                            {
                                AuthorID_ = x.AuthorID,
                                Name_ = x.Name,
                                Nationality_ = x.Nationality,
                                BirthDate_ = x.BirthDate,
                                Sex_ = x.Sex,
                                Description_ = x.Description,
                            }).SingleOrDefault();
                return author;
            }
        }
        public static List<BookHouse_> getAlLBookHouse()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var bookhouses = (from u in _context.BookHouse_.AsEnumerable()
                               select new
                               {
                                   BhID = u.BhID_,
                                   Name = u.Name_,
                                   Address = u.Address_,
                                   Description = u.Description_,
                               })
                            .Select(x => new BookHouse_
                            {
                                BhID_ = x.BhID,
                                Name_ = x.Name,
                                Address_ = x.Address,
                                Description_ = x.Description,
                            }).ToList();
                return bookhouses;
            }
        }
        public static BookHouse_ getABookHouse(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var bookhouse = (from u in (from i in _context.BookHouse_.AsEnumerable()
                                         select i)
                              where u.BhID_ == id
                              select new
                              {
                                  BhID = u.BhID_,
                                  Name = u.Name_,
                                  Address = u.Address_,
                                  Description = u.Description_,
                              })
                            .Select(x => new BookHouse_
                            {
                                BhID_ = x.BhID,
                                Name_ = x.Name,
                                Address_ = x.Address,
                                Description_ = x.Description,
                            }).SingleOrDefault();
                return bookhouse;
            }
        }
        public static List<Customer_> getAllCustomer()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var customers = (from u in _context.Customer_.AsEnumerable()
                                  select new
                                  {
                                      CustomerID = u.CustomerID_,
                                      Name = u.Name_,
                                      BirthDate=u.BirthDate_,
                                      Sex=u.Sex_,
                                      Phone=u.Phone_,
                                      Address = u.Address_,
                                  })
                            .Select(x => new Customer_
                            {
                                CustomerID_ = x.CustomerID,
                                Name_ = x.Name,
                                BirthDate_ = x.BirthDate,
                                Sex_ = x.Sex,
                                Phone_ = x.Phone,
                                Address_ = x.Address,
                            }).ToList();
                return customers;
            }
        }
        public static Customer_ getACustomer(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var customer = (from u in (from i in _context.Customer_.AsEnumerable()
                                            select i)
                                 where u.CustomerID_ == id
                                 select new
                                 {
                                     CustomerID = u.CustomerID_,
                                     Name = u.Name_,
                                     BirthDate = u.BirthDate_,
                                     Sex = u.Sex_,
                                     Phone = u.Phone_,
                                     Address = u.Address_,
                                 })
                            .Select(x => new Customer_
                            {
                                CustomerID_ = x.CustomerID,
                                Name_ = x.Name,
                                BirthDate_ = x.BirthDate,
                                Sex_ = x.Sex,
                                Phone_ = x.Phone,
                                Address_ = x.Address,
                            }).SingleOrDefault();
                return customer;
            }
        }
        public static List<Account_> getAllAccount()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var accounts = (from u in _context.Account_.AsEnumerable()
                                select new
                                {
                                    AccID = u.AccID_,
                                    Username = u.Username_,
                                    Email = u.Email_,
                                    Password = u.Password_,
                                    DateCreated = u.DateCreated_,
                                    Status = u.Status_,
                                    CustomerID = u.CustomerID_,
                                    AuID = u.AuID_,
                                })
                            .Select(x => new Account_
                            {
                                AccID_ = x.AccID,
                                Username_ = x.Username,
                                Email_ = x.Email,
                                Password_ = x.Password,
                                DateCreated_ = x.DateCreated,
                                Status_ = x.Status,
                                CustomerID_ = x.CustomerID,
                                AuID_ = x.AuID,
                            }).ToList();
                return accounts;
            }
        }
        public static Account_ getAAccount(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var account = (from u in (from i in _context.Account_.AsEnumerable()
                                           select i)
                                where u.AccID_ == id
                                select new
                                {
                                    AccID = u.AccID_,
                                    Username = u.Username_,
                                    Email = u.Email_,
                                    Password = u.Password_,
                                    DateCreated = u.DateCreated_,
                                    Status = u.Status_,
                                    CustomerID=u.CustomerID_,
                                    AuID=u.AuID_,
                                })
                            .Select(x => new Account_
                            {
                                AccID_ = x.AccID,
                                Username_ = x.Username,
                                Email_ = x.Email,
                                Password_ = x.Password,
                                DateCreated_ = x.DateCreated,
                                Status_ = x.Status,
                                CustomerID_ = x.CustomerID,
                                AuID_ = x.AuID,
                            }).SingleOrDefault();
                return account;
            }
        }
        public static List<Authorization_> getAllAuthorization()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var Authorization = (from u in _context.Authorization_.AsEnumerable()
                                 select new
                                 {
                                     AuID = u.AuID_,
                                     AuName = u.AuName_,
                                     Desctiption=u.Description_,
                                 })
                            .Select(x => new Authorization_
                            {
                                AuID_ = x.AuID,
                                AuName_ = x.AuName,
                                Description_=x.Desctiption,
                            }).ToList();
                return Authorization;
            }
        }
        public static Authorization_ getAAuthorization(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var Authorization = (from u in (from i in _context.Authorization_.AsEnumerable()
                                            select i)
                                 where u.AuID_ == id
                                     select new
                                     {
                                         AuID = u.AuID_,
                                         AuName = u.AuName_,
                                         Desctiption = u.Description_,
                                     })
                            .Select(x => new Authorization_
                            {
                                AuID_ = x.AuID,
                                AuName_ = x.AuName,
                                Description_ = x.Desctiption,
                            }).SingleOrDefault();
                return Authorization;
            }
        }
        public static List<PayMethod_> getAllPayMethod()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var paymethod = (from u in _context.PayMethod_.AsEnumerable()
                                 select new
                                 {
                                     PayID = u.PayID_,
                                     Name = u.Name_,
                                 })
                            .Select(x => new PayMethod_
                            {
                                PayID_ = x.PayID,
                                Name_ = x.Name,
                            }).ToList();
                return paymethod;
            }
        }
        public static PayMethod_ getAPayMethod(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var paymethod = (from u in (from i in _context.PayMethod_.AsEnumerable()
                                           select i)
                                where u.PayID_ == id
                                select new
                                {
                                    PayID = u.PayID_,
                                    Name = u.Name_,
                                })
                            .Select(x => new PayMethod_
                            {
                                PayID_ = x.PayID,
                                Name_ = x.Name,
                            }).SingleOrDefault();
                return paymethod;
            }
        }
        public static List<PurchaseOrders_> getAllPurchaseOrders()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PurchaseOrderss = (from u in _context.PurchaseOrders_.AsEnumerable()
                               select new
                               {
                                   PoID = u.PoID_,
                                   OrderDate = u.OrderDate_,
                                   DeliveryDate = u.DeliveryDate_,
                                   Status = u.Status_,
                                   PayID = u.PayID_,
                               })
                            .Select(x => new PurchaseOrders_
                            {
                                PoID_ = x.PoID,
                                OrderDate_ = x.OrderDate,
                                DeliveryDate_ = x.DeliveryDate,
                                Status_ = x.Status,
                                PayID_ = x.PayID,
                            }).ToList();
                return PurchaseOrderss;
            }
        }
        public static PurchaseOrders_ getAPurchaseOrders(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PurchaseOrders = (from u in (from i in _context.PurchaseOrders_.AsEnumerable()
                                       select i)
                            where u.PoID_ == id
                                      select new
                                      {
                                          PoID = u.PoID_,
                                          OrderDate = u.OrderDate_,
                                          DeliveryDate = u.DeliveryDate_,
                                          Status = u.Status_,
                                          PayID = u.PayID_,
                                      })
                            .Select(x => new PurchaseOrders_
                            {
                                PoID_ = x.PoID,
                                OrderDate_ = x.OrderDate,
                                DeliveryDate_ = x.DeliveryDate,
                                Status_ = x.Status,
                                PayID_ = x.PayID,
                            }).SingleOrDefault();
                return PurchaseOrders;
            }
        }
        public static List<PurchaseOrderDetails_> getAllPurchaseOrderDetails()
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PurchaseOrderDetailss = (from u in _context.PurchaseOrderDetails_.AsEnumerable()
                                       select new
                                       {
                                           PoID = u.PoID_,
                                           CustomerID = u.CustomerID_,
                                           BookID = u.BookID_,
                                           Quantity = u.Quantity,
                                           Cost = u.Cost,
                                       })
                            .Select(x => new PurchaseOrderDetails_
                            {
                                PoID_ = x.PoID,
                                CustomerID_ = x.CustomerID,
                                BookID_ = x.BookID,
                                Quantity = x.Quantity,
                                Cost = x.Cost,
                            }).ToList();
                return PurchaseOrderDetailss;
            }
        }
        public static PurchaseOrderDetails_ getAPurchaseOrderDetails(int id)
        {
            using (var _context = new BEERBOOKEntities())
            {
                var PurchaseOrderDetails = (from u in (from i in _context.PurchaseOrderDetails_.AsEnumerable()
                                                 select i)
                                      where u.PoID_ == id
                                            select new
                                            {
                                                PoID = u.PoID_,
                                                CustomerID = u.CustomerID_,
                                                BookID = u.BookID_,
                                                Quantity = u.Quantity,
                                                Cost = u.Cost,
                                            })
                            .Select(x => new PurchaseOrderDetails_
                            {
                                PoID_ = x.PoID,
                                CustomerID_ = x.CustomerID,
                                BookID_ = x.BookID,
                                Quantity = x.Quantity,
                                Cost = x.Cost,
                            }).SingleOrDefault();
                return PurchaseOrderDetails;
            }
        }
    }
}