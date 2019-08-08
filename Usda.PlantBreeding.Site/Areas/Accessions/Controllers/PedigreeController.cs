using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using BSGUtils;

namespace Usda.PlantBreeding.Site.Areas.Accessions.Controllers
{
    [ActionFilters.AuthActionFilters]
    public class PedigreeController : Controller
    {
        private IPlantBreedingRepo m_repo;

        public PedigreeController() : this(new PlantBreedingRepo()) { }

        public PedigreeController(IPlantBreedingRepo repo)
        {
            m_repo = repo;
        }

        // GET: Pedigree
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tree(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Constants needed by the tree
            int treeSize = 15;
            int index = 1; //Current location in the array

            //root of tree, child to build off of
            Genotype root = m_repo.GetGenotype(id.Value);
            if (root == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //Tree to pass to page
            List<Genotype> tree = new List<Genotype>();
            List<Family> fams = new List<Family>();

            //fill list to handle off array errors 
            for (int i = 0; i < treeSize; i++)
            {
                tree.Add(new Genotype { GivenName = "" });

            }
            
            //fill list to handle off array errors
            for (int i = 0; i < treeSize; i++)
            {
                fams.Add(new Family());
            }

            //add male parent
            tree.Insert(index, root);
            fams.Insert(index, m_repo.GetFamily(root.FamilyId));

            //Increment index
            index++;
            
            //Get root parents
            Tuple<Genotype, Genotype> parents = m_repo.GetGenotypeParents(root);

            //different iteration to handle 0 index
            //Female is always first in the tuple
            if (parents.Item1 != null)
            {
                tree.Insert(index, parents.Item1);
                fams.Insert(index, m_repo.GetFamily(parents.Item1.FamilyId));
            }
            else
            {
                tree.Insert(index, new Genotype { GivenName = "" });
                fams.Insert(index, new Family() { });
            }

            //Male is second
            if (parents.Item2 != null)
            {
                tree.Insert(index + 1, parents.Item2);
                fams.Insert(index + 1, m_repo.GetFamily(parents.Item2.FamilyId));
            }
            else
            {
                tree.Insert(index + 1, new Genotype { GivenName = "" });
                fams.Insert(index + 1, new Family() {  });
            }

            //Tree population, through grandparents (index 8), start at father (index 2)
            for (int i = 2; i < 8; i++)
            {
                if (!tree[i].Id.Equals(0)) //if parent is not null
                {
                    parents = m_repo.GetGenotypeParents(tree[i]); //get parents
                }
                else
                {
                    parents = new Tuple<Genotype, Genotype>(item1: null, item2: null); //new tuple with null genotypes
                }

                addParents(ref index, ref tree, ref fams, parents); 
            }

            //Send list to view
            tree.RemoveAt(0);
            fams.RemoveAt(0);

            //tree.RemoveRange(15, tree.Count - 15);
            ViewBag.genotypeList = tree;
            ViewBag.families = fams;
            ViewBag.Genotype = root;
            return View();
        }

        //Function adds parents to the tree, if parents are null, add blank genotypes
        public void addParents(ref int index, ref List<Genotype> tree, ref List<Family> fams, Tuple<Genotype, Genotype> parents) 
        {
            if (parents.Item1 != null)
            {
                tree.Insert(index * 2, parents.Item1);
                fams.Insert(index * 2, m_repo.GetFamily(parents.Item1.FamilyId));
            }
            else
            {
                tree.Insert(index * 2, new Genotype { GivenName = "" });
                fams.Insert(index * 2, new Family() {  });
            }

            if (parents.Item2 != null)
            {
                tree.Insert(index * 2 + 1, parents.Item2);
                fams.Insert(index * 2 + 1, m_repo.GetFamily(parents.Item2.FamilyId));
            }
            else
            {
                tree.Insert(index * 2 + 1, new Genotype { GivenName = "" });
                fams.Insert(index * 2 + 1, new Family() { });
            }

            index++;
        }
        
    }
}