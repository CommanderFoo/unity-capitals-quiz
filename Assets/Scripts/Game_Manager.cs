using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace net.pixeldepth {

	public class Game_Manager : MonoBehaviour {
    
		public static Game_Manager instance;

		public TextMeshProUGUI question;

		[Space]

		public Color correct_color = Color.yellow;
		public Color incorrect_color = Color.red;
		public Color other_color = Color.grey;

		[Space]

		public RectTransform info_rect;
		public CanvasGroup info_grp;
		public TextMeshProUGUI info_text;

		[Space]

		public RectTransform next_rect;
		public CanvasGroup next_grp;

		[Space]

		public TextMeshProUGUI best_streak_text;
		public TextMeshProUGUI current_streak_text;

		[Space]

		public Answer[] answers;

		[Space]

		public int correct_answer = -1;

		private bool picked = false;
		
		private int current_streak = 0;
		private int best_streak = 0;

		private void Awake(){
			if(instance != null && instance != this){
				return;
			}

			instance = this;

			if(PlayerPrefs.HasKey("best_streak")){
				this.best_streak = PlayerPrefs.GetInt("best_streak");
				this.best_streak_text.text = this.best_streak.ToString();
			}

			if(PlayerPrefs.HasKey("current_streak")){
				this.current_streak = PlayerPrefs.GetInt("current_streak");
				this.current_streak_text.text = this.current_streak.ToString();
			}
		}

		private void Start(){
			Screen.fullScreen = false;

			this.show_question();
		}

		private void Update(){
			if(Input.GetKeyDown(KeyCode.Escape)){
				this.exit_game();
			}
		}

		public void exit_game(){
			if(Application.platform != RuntimePlatform.WebGLPlayer){
				Application.Quit();
			}
		}

		public void show_question(){
			this.hide_next();
			this.close_info();

			for(int i = 0; i < this.answers.Length; i ++){
				this.answers[i].hide_info_button();
			}

			this.picked = false;

			Scriptable_Country country = Quiz_Manager.instance.get_country();

			Game_Manager.instance.set_question(country.country);

			List<Scriptable_Country> a = Quiz_Manager.instance.get_random_answers();

			this.correct_answer = Quiz_Manager.instance.current_index;

			for(int i = 0; i < a.Count; i ++){
				this.answers[i].set_answer(a[i]);
			}
		}
			   
		public void set_question(string country){
			this.question.text = "What is the capital of <color=yellow>" + country + "</color>?";
		}
		
		public void show_correct_answer(Answer clicked_answer){
			if(this.picked){
				return;
			}

			this.picked = true;

			bool answered = false;

			if(clicked_answer.index == this.correct_answer){
				clicked_answer.correct_color();
				answered = true;
				this.current_streak ++;

				if(this.current_streak > this.best_streak){
					this.best_streak = this.current_streak;
					this.best_streak_text.text = this.best_streak.ToString();

					PlayerPrefs.SetInt("best_streak", this.best_streak);
				}
			} else {
				clicked_answer.incorrect_color();
				this.current_streak = 0;
			}

			for(int i = 0; i < this.answers.Length; i ++){
				if(this.answers[i].index != clicked_answer.index){
					if(!answered){
						if(this.answers[i].index == this.correct_answer){
							this.answers[i].correct_color(false);
						} else {
							this.answers[i].incorrect_color();
						}
					} else {
						this.answers[i].other_color();
					}
				}

				this.answers[i].show_info_button();
			}

			this.show_next();
			this.current_streak_text.text = this.current_streak.ToString();
			
			PlayerPrefs.SetInt("current_streak", this.current_streak);
		}

		public void show_info(string text){
			this.info_text.text = text;
			this.info_rect.gameObject.SetActive(true);
			this.info_grp.DOFade(1f, .3f);
			this.info_rect.DOPunchScale(new Vector3(.1f, .1f, .1f), .2f);
		}

		public void close_info(){
			this.info_grp.alpha = 0;
			this.info_rect.gameObject.SetActive(false);
		}

		public void show_next(){
			this.next_rect.gameObject.SetActive(true);
			this.next_grp.DOFade(1f, 1f);
		}

		public void hide_next(){
			this.next_grp.DOFade(0f, .3f).OnComplete(() => this.next_rect.gameObject.SetActive(false));
		}

		public void next(){
			this.show_question();
		}

	}

}