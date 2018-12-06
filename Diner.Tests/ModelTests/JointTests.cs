using Microsoft.VisualStudio.TestTools.UnitTesting;
using Diner.Models;
using System.Collections.Generic;
using System;
 
namespace Diner.Tests
{
 [TestClass]
 public class JointTest : IDisposable
 {
     public void Dispose()
     {
         Joint.ClearAll();
     }
      public JointTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=diners_test;";
    }

    [TestMethod]
    public void Edit_UpdatesJointNameInDatabase_String()
    {
      //Arrange
      Joint testJoint = new Joint("Burger King", 1, "terrible burgers");
      testJoint.Save();
      
      string newNotes = "great fondue";
      string newName = "The Melting Pot";

      //Act
      testJoint.Edit(newName, newNotes);
      string resultNotes = Joint.Find(testJoint.GetId()).GetNotes();
      string resultName = Joint.Find(testJoint.GetId()).GetName();
      //Assert
      Assert.AreEqual(newName, resultName);
    }

    [TestMethod]
    public void Edit_UpdatesJointNotesInDatabase_String()
    {
      //Arrange
      Joint testJoint = new Joint("Burger King", 1, "terrible burgers");
      testJoint.Save();
      
      string newNotes = "great fondue";
      string newName = "The Melting Pot";

      //Act
      testJoint.Edit(newName, newNotes);
      string resultNotes = Joint.Find(testJoint.GetId()).GetNotes();
      string resultName = Joint.Find(testJoint.GetId()).GetName();
      //Assert
      Assert.AreEqual(newNotes, resultNotes);
    }

  }

}

