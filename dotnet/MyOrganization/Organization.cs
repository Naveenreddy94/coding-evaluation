﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            Position position = FindPosition(title, root);
            if (position != null && !position.IsFilled())
            {
                position.SetEmployee(new Employee(person));
                return position;
            }
            return null;
        }

        private Position FindPosition(string title, Position node)
        {
            if (node.GetTitle() == title)
            {
                return node;
            }
            foreach (Position directReport in node.GetDirectReports())
            {
                Position found = FindPosition(title, directReport);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
