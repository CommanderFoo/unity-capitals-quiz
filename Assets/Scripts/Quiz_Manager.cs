using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace net.pixeldepth {


	public class Quiz_Manager : MonoBehaviour {
    
		public static Quiz_Manager instance;

		public int current_index = 0;
		public int previous_index = 0;

		public List<Scriptable_Country> countries;
	
		private void Awake(){
			if(instance != null && instance != this){
				return;
			}

			instance = this;
		}

		public Scriptable_Country get_country(){
			int index = 0;

			do {

				index = UnityEngine.Random.Range(0, this.countries.Count);

			} while(index == this.current_index || index == this.previous_index);
			
			this.previous_index = this.current_index;
			this.current_index = index;
			
			return this.countries[index];
		}

		public List<Scriptable_Country> get_random_answers(){
			List<Scriptable_Country> answers = new List<Scriptable_Country>(5);

			// 1

			int answer_1 = this.current_index;
			Scriptable_Country country_1 = this.countries[answer_1];
			country_1.index = answer_1;

			answers.Add(country_1);

			// 2

			int answer_2 = 0;

			do {

				answer_2 = UnityEngine.Random.Range(0, this.countries.Count);

			} while(answer_2 == answer_1 || answer_2 == this.previous_index);

			Scriptable_Country country_2 = this.countries[answer_2];
			country_2.index = answer_2;

			answers.Add(country_2);
			
			// 3

			int answer_3 = 0;

			do {

				answer_3 = UnityEngine.Random.Range(0, this.countries.Count);

			} while(answer_3 == answer_1 || answer_3 == this.previous_index || answer_3 == answer_2);

			Scriptable_Country country_3 = this.countries[answer_3];
			country_3.index = answer_3;

			answers.Add(country_3);

			// 4

			int answer_4 = 0;

			do {

				answer_4 = UnityEngine.Random.Range(0, this.countries.Count);

			} while(answer_4 == answer_1 || answer_4 == this.previous_index || answer_4 == answer_2 || answer_4 ==  answer_3);

			Scriptable_Country country_4 = this.countries[answer_4];
			country_4.index = answer_4;

			answers.Add(country_4);

			// 5

			int answer_5 = 0;

			do {

				answer_5 = UnityEngine.Random.Range(0, this.countries.Count);

			} while(answer_5 == answer_1 || answer_5 == this.previous_index || answer_5 == answer_2 || answer_5 ==  answer_3 || answer_5 == answer_4);

			Scriptable_Country country_5 = this.countries[answer_5];
			country_5.index = answer_5;

			answers.Add(country_5);

			// Random order and return back

			answers = answers.OrderBy(a => Guid.NewGuid()).ToList();

			return answers;
		}

	}

}