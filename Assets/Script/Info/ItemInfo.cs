using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class SkillInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public int Range { get; set; }
        public string Describe { get; set; }
        public string Icon { get; set; }

        public string Action{ get; set; }
    public SkillInfo()
        {
            this.ID = -1;
        }


    }
    public class ConsumablesInfo
    {
        public int ID { get; set; }
      
        public ConsumablesInfo()
        {
            this.ID = -1;
        }


    }

