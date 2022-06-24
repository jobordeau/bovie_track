using Modele;
using Data;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest_Tags
    {
        [Fact]
        public void TestAjoutNouveauTag()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.AjouterNouveauTag("Test");

            Assert.Throws<ArgumentException>(() => manager.AjouterNouveauTag(""));
            Assert.Contains("Test", manager.collectionTags); //Test dans la liste global
        }

        [Fact]
        public void TestAssociertag()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();

            Assert.Throws<ArgumentException>(() => manager.AssocierTag(manager.GetOeuvre(1), "Test"));
            manager.AjouterNouveauTag("Test");
            Assert.True(manager.AssocierTag(manager.GetOeuvre(1), "Test"));
            Assert.False(manager.AssocierTag(manager.GetOeuvre(1), "Test"));
            Assert.Contains("Test", manager.collectionTags); //Test dans la liste global
            Assert.Contains("Test", manager.GetOeuvre(1).Tags); //Test dans la liste de l'oeuvre
        }

        [Fact]
        public void TestEnleverTag()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            Assert.False(manager.EnleverTag(manager.GetOeuvre(1), "Test"));
            manager.AjouterNouveauTag("Test");
            manager.AssocierTag(manager.GetOeuvre(1), "Test");
            Assert.True(manager.EnleverTag(manager.GetOeuvre(1), "Test"));
            Assert.DoesNotContain("Test", manager.GetOeuvre(1).Tags); //Test dans la liste de l'oeuvre
        }

        [Fact]
        public void TestSupprimerUnTag()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            Assert.False(manager.SupprimerUnTag("Test"));
            manager.AjouterNouveauTag("Test");
            Assert.True(manager.SupprimerUnTag("Test"));

            Assert.DoesNotContain("Test", manager.collectionTags); //Test dans la liste globale
        }

        [Fact]
        public void CompterAssociationsTag()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.SupprimerUnTag("Tag 3");
            manager.AssocierTag(manager.GetOeuvre(1), "Tag 1");
            manager.AssocierTag(manager.GetOeuvre(3), "Tag 1");
            manager.AssocierTag(manager.GetOeuvre(5), "Tag 1");

            Assert.Equal(8, manager.collectionTags.Count); //Test dans la liste globale
            Assert.Equal(3, manager.CompterAssociationsTag("Tag 1")); //Test dans la liste globale
        }
    }
}
