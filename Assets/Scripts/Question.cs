using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question 
{
    public int id { get; set; }
    public string questiontext { get; set; }
    
   public Question(int id, string questiontext)
   {
       this.id = id;
       this.questiontext = questiontext;

   }
}
