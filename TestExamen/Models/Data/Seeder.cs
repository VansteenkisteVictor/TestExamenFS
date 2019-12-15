using Models.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Data
{
    public class Seeder : ISeeder
    {
        private readonly ICommentRepo commentRepo;
        private readonly IDetailRepo detailsRepo;
        private readonly CommentsDBContext commentsDBContext;
        //invullen via property dependancy vanuit API project >> startup.cs >> configure 
        public List<Guid> Lst_RestaurantGuids { get; set; } = new List<Guid>();
        //init !! 
        public Seeder(ICommentRepo commentRepo, IDetailRepo detailsRepo, CommentsDBContext commentsDBContext)
        {
            this.commentRepo = commentRepo;
            this.detailsRepo = detailsRepo;
            this.commentsDBContext = commentsDBContext;
        }
        public void initDatabase(int nmbrComments)
        {
            //geen data blijven toevoegen 
            try
            {
                if (!commentsDBContext.CollectionExistsAsync("comments").Result)
                {
                    //1. Comments aanmaken
                    for (var i = 0; i < nmbrComments; i++)
                    {
                        Lst_RestaurantGuids.Add(Guid.NewGuid());
                        commentRepo.CreateAsync(new Models.Comment
                        {
                            Quotation = 4 + new Random().Next(5),
                            RestaurantID = Lst_RestaurantGuids[i]
                        });
                    }
                }  //2.CommentDetails aanmaken (collective namen zijn case sensitive) 
                if (!commentsDBContext.CollectionExistsAsync("details").Result)
                {
                    detailsRepo.CreateAsync(new Models.CommentDetails { Id = new MongoDB.Bson.ObjectId(), RestaurantID = Lst_RestaurantGuids[new Random().Next(Lst_RestaurantGuids.Count)], LocationCity = "Brugge", Coordinates = new string[] { "51.13", "3.14" }, Subject = "Pricing", ExtraInfo = "Too expensive" });
                    detailsRepo.CreateAsync(new Models.CommentDetails { Id = new MongoDB.Bson.ObjectId(), RestaurantID = Lst_RestaurantGuids[new Random().Next(Lst_RestaurantGuids.Count)], LocationCity = "Kortrijk", Coordinates = new string[] { "50.50", "3.16" }, Subject = "Service", ExtraInfo = "Excellent" });
                    //verder aan te vullen met details indien gewenst 
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("fout bij het seeden:", exc);
            }
        }
    }
}
