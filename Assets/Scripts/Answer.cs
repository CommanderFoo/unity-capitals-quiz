using TMPro;
using UnityEngine;
using DG.Tweening;

namespace net.pixeldepth {

	public class Answer : MonoBehaviour {

		public TextMeshProUGUI answer_text;
		public CanvasGroup info_button;

		[Space]

		public CanvasGroup check_grp;

		public int index = -1;

		private Scriptable_Country country;

		public void set_answer(Scriptable_Country country){
			this.index = country.index;
			this.country = country;

			this.answer_text.text = country.capital;
			this.answer_text.color = Game_Manager.instance.other_color;
		}
		
		public void pointer_click(){
			Game_Manager.instance.show_correct_answer(this);
		}

		public void correct_color(bool show_check = true){
			this.answer_text.color = Game_Manager.instance.correct_color;

			if(show_check){
				this.check_grp.gameObject.SetActive(true);
				this.check_grp.DOFade(1f, 1f);
			}
		}
		
		public void incorrect_color(){
			this.answer_text.color = Game_Manager.instance.incorrect_color;
		}

		public void other_color(){
			this.answer_text.color = Game_Manager.instance.other_color;
		}

		public void show_info_button(){
			this.info_button.gameObject.SetActive(true);
			this.info_button.DOFade(1f, 1f);
		}

		public void hide_info_button(){
			this.check_grp.DOFade(0f, .3f).OnComplete(() => this.check_grp.gameObject.SetActive(false));
			this.info_button.DOFade(0f, .3f).OnComplete(() => this.info_button.gameObject.SetActive(false));
		}

		public void info_click(){
			Game_Manager.instance.show_info(this.country.info);
		}

	}

}
