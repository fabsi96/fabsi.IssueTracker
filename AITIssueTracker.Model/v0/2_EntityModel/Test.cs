﻿using System;
using System.Collections.Generic;
using System.Text;
using AITIssueTracker.Model.v0._1_FormModel;
using AITIssueTracker.Model.v0._3_ViewModel;
using Npgsql;

namespace AITIssueTracker.Model.v0._2_EntityModel
{
    public class Test : IViewModel<TestView>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime BirthDate { get; set; }

        public MyType Status { get; set; }

        public Test()
        {
            
        }

        public Test(TestForm item)
        {
            Name = item.Name;
            Age = item.Age;
            BirthDate = item.BirthDate;
            Status = item.Status;
        }

        public Test(NpgsqlDataReader reader)
        {
            if (reader is null)
                throw new Exception("Test(NpgsqlDataReader): Error: reader is null.");

            Name = reader["name"].ToString();
            Age = int.Parse(reader["age"].ToString());
            BirthDate = DateTime.Parse(reader["birth_date"].ToString());
            // TODO: Check if its working
            Status = (MyType) Enum.Parse(typeof(MyType), reader["status"].ToString());
        }

        public TestView AsView()
        {
            return new TestView
            {
                Name = Name,
                Age = Age,
                BirthDate = BirthDate,
                Status = Status,
            };
        }
    }
}
