using Data;
using Modele;
using Xunit;

namespace UnitTests
{
    public class UnitTest_Tris
    {
        [Fact]
        public void TestTriAlphabétique()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 9, 10, 11,12, 5, 6, 7, 8, 1, 2, 3, 4};
            foreach(Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
            
        }

        [Fact]
        public void TestTriDateCroissante()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.mode = ModeDeTri.DateCroissante;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 12, 11, 8, 7, 4, 3, 1, 2, 9, 10, 5, 6};
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }

        }

        [Fact]
        public void TestTriDateDecroissante()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.mode = ModeDeTri.DateDecroissante;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 5, 6, 9, 10, 1, 2, 3, 4, 7, 8, 11, 12 };
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriTermine()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.Termine = !manager.CollectionMgr.Termine;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 11, 12, 7, 8, 3, 4};
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriEnCours()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.EnCours = !manager.CollectionMgr.EnCours;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 9, 11, 5, 7, 1, 3};
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriNonCommence()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.NonCommence = !manager.CollectionMgr.NonCommence;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 9, 10, 12, 5, 6, 8, 1, 2, 4};
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriPlanifie()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.Planifie = !manager.CollectionMgr.Planifie;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 9, 10, 12, 5, 6, 8, 1, 2, 4 };
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriNonPlanifie()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.NonPlanifie = !manager.CollectionMgr.NonPlanifie;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 11, 7,3 };
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriLivre()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.TypeLivre = !manager.CollectionMgr.TypeLivre;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 5, 6, 7, 8, 1, 2, 3, 4 };
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriSerie()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.TypeSerie = !manager.CollectionMgr.TypeSerie;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie();
            int i = 0;
            int[] triValide = { 9, 10, 11, 12, 1, 2, 3, 4 };
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }

        [Fact]
        public void TestTriFilm()
        {
            Manager manager = new Manager(new Stub());
            manager.ChargeDonnees();
            manager.CollectionMgr.TypeFilm = !manager.CollectionMgr.TypeFilm;
            manager.CollectionMgr.mode = ModeDeTri.Alphabetique;
            manager.ActualiseTrie(); ;
            int i = 0;
            int[] triValide = { 9, 10, 11, 12, 5, 6, 7, 8};
            foreach (Oeuvre o in manager.collectionAffichee)
            {
                Assert.Equal(triValide[i++], o.ID);
            }
        }


    }
}
