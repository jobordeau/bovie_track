using Modele;
using Data;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace UnitTests
{
    public class UnitTest_Oeuvre
    {
        [Fact]
        public void TestAjouterOeuvre()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            Assert.Equal(1, manager.GetOeuvre(1).ID);
        }

        [Fact]
        public void TestModifierFilm()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.ModifierFilm(manager.GetOeuvre(1) as Film, "Test", new DateTime(2019, 8, 12), new DateTime(2024, 5, 13), 2, new TimeSpan(2, 19, 0), new TimeSpan(1, 5, 0), 5, "Pas ouf");
            Film test = manager.GetOeuvre(1) as Film;

            Assert.Equal("Test", test.Nom);
            Assert.Equal(new DateTime(2019, 8, 12), test.DateFini);
            Assert.Equal(new DateTime(2024, 5, 13), test.DatePrevue);
            Assert.True(2 == test.NbFini);
            Assert.Equal(new TimeSpan(2, 19, 0), test.Duree);
            Assert.Equal(new TimeSpan(1, 5, 0), test.Etat);
            Assert.Equal(5, test.Avis.Note);
            Assert.Equal("Pas ouf", test.Avis.Commentaire);
        }

        [Fact]
        public void TestModifierLivre()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.ModifierLivre(manager.GetOeuvre(9) as Livre, "Test", new DateTime(2019, 8, 12), new DateTime(2024, 5, 13), 2, 500, "JK Rolling", 107, 5, "Pas ouf");
            Livre test = manager.GetOeuvre(9) as Livre;

            Assert.Equal("Test", test.Nom);
            Assert.Equal(new DateTime(2019, 8, 12), test.DateFini);
            Assert.Equal(new DateTime(2024, 5, 13), test.DatePrevue);
            Assert.True(2 == test.NbFini);
            Assert.True(500 == test.NbPages);
            Assert.Equal(107, test.Etat);
            Assert.Equal(5, test.Avis.Note);
            Assert.Equal("Pas ouf", test.Avis.Commentaire);
        }

        [Fact]
        public void TestModifierSerie()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.ModifierSerie(manager.GetOeuvre(5) as Serie, "Test", new DateTime(2019, 8, 12), new DateTime(2024, 5, 13), 2, 4, 2, 2, new float?[] { 1f, 3f, 5f, 7f }, new string[] { "test 1", "Test 2", "Test 3", "Test 4" });
            Serie test = manager.GetOeuvre(5) as Serie;


            Assert.Equal("Test",test.Nom);
            Assert.Equal(new DateTime(2019, 8, 12), test.DateFini);
            Assert.Equal(new DateTime(2024, 5, 13), test.DatePrevue);
            Assert.True(2 == test.NbFini);
            Assert.True(4 == test.NbSaisons);
            Assert.Equal(2, test.Etat.Saison);
            Assert.Equal(2, test.Etat.Episode);
            Assert.Contains(new Avis(1, "test 1"), test.ListeAvis);
        }

        [Fact]
        public void TestValidateur()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            IEnumerable<Invalide> erreurs;
            List<Invalide> toutesErreurs = new List<Invalide>() {Invalide.nom,Invalide.datePrevue,Invalide.dateFini,
            Invalide.etat, Invalide.avis, };

            erreurs = manager.AjouterFilm(" ", new DateTime(2080, 8, 8), new DateTime(1995, 5, 5), 2, new TimeSpan(2,15,0), new TimeSpan(8, 0, 0), -2f, null);

            Assert.True(erreurs.Intersect(toutesErreurs).Count() == toutesErreurs.Count());

            erreurs = manager.AjouterSerie(null, new DateTime(2080, 8, 8), new DateTime(1995, 5, 5), 0, 2, 10, 2, new float?[] { 4f, null }, new string[] { null });
            Assert.True(erreurs.Intersect(toutesErreurs).Count() == toutesErreurs.Count());

            toutesErreurs.Add(Invalide.auteur);
            toutesErreurs.Remove(Invalide.dateFini);
            toutesErreurs.Remove(Invalide.datePrevue);
            toutesErreurs.Remove(Invalide.nom);
            erreurs = manager.AjouterLivre("Test", null, new DateTime(1995, 5, 5), 45, 125, " ", 999, 45.2f, "test");
            Assert.True(erreurs.Intersect(toutesErreurs).Count() == toutesErreurs.Count());
        }


        [Fact]
        public void TestSupprimerOeuvre()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            Assert.True(manager.SupprimerOeuvre(manager.GetOeuvre(2)));
            Assert.Throws<ArgumentException>(() => manager.GetOeuvre(2));
        }
    }
}
