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
        public void TestValidateur()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            IEnumerable<Invalide> erreurs;
            List<Invalide> toutesErreurs = new List<Invalide>() {Invalide.nom,Invalide.datePrevue,Invalide.dateFini,
            Invalide.etat, Invalide.avis, };

            erreurs = manager.AjouterFilm(" ", "img", new DateTime(2080, 8, 8), new DateTime(1995, 5, 5), 2, new TimeSpan(2, 15, 0), new TimeSpan(8, 0, 0), -2f, null);

            Assert.True(erreurs.Intersect(toutesErreurs).Count() == toutesErreurs.Count());

            erreurs = manager.AjouterSerie(null, "img", new DateTime(2080, 8, 8), new DateTime(1995, 5, 5), 0, 2, 10, 2, new float?[] { 4f, null }, new string[] { null });
            Assert.True(erreurs.Intersect(toutesErreurs).Count() == toutesErreurs.Count());

            toutesErreurs.Add(Invalide.auteur);
            toutesErreurs.Remove(Invalide.dateFini);
            toutesErreurs.Remove(Invalide.datePrevue);
            toutesErreurs.Remove(Invalide.nom);
            erreurs = manager.AjouterLivre("Test", "img", null, new DateTime(1995, 5, 5), 45, 125, " ", 999, 45.2f, "test");
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
