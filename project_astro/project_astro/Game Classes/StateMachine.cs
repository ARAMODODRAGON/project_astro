using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_astro {
	class StateMachine<T> where T : Enum {
		// delegate definitions
		public delegate void UpdateState(float delta);
		public delegate void ChangeState(T fromtostate);

		// dictionaries that store the functions per state
		Dictionary<T, UpdateState> updateDel;
		Dictionary<T, ChangeState> enterDel;
		Dictionary<T, ChangeState> exitDel;

		// state
		private T LastState;
		public T State { get; private set; }

		public StateMachine() {
			//construct delegate dictionaries
			updateDel = new Dictionary<T, UpdateState>();
			enterDel = new Dictionary<T, ChangeState>();
			exitDel = new Dictionary<T, ChangeState>();
		}

		public void Update(float delta) {
			if (updateDel.Count > 0 && updateDel.ContainsKey(State)) {
				updateDel[State].Invoke(delta);
			}

			if (!State.Equals(LastState)) {
				if (exitDel.Count > 0 && exitDel.ContainsKey(LastState)) {
					exitDel[LastState].Invoke(State);
				}
				if (enterDel.Count > 0 && enterDel.ContainsKey(State)) {
					enterDel[State].Invoke(LastState);
				}
				LastState = State;
			}
		}
		
		public void SetState(T newstate) {
			State = newstate;
		}

		#region delegates setters

		public void AddEntry(T key, ChangeState function) {
			if (enterDel.ContainsKey(key)) {
				enterDel[key] = function;
			} else {
				enterDel.Add(key, function);
			}
		}

		public void AddUpdate(T key, UpdateState function) {
			if (updateDel.ContainsKey(key)) {
				updateDel[key] = function;
			} else {
				updateDel.Add(key, function);
			}
		}

		public void AddExit(T key, ChangeState function) {
			if (exitDel.ContainsKey(key)) {
				exitDel[key] = function;
			} else {
				exitDel.Add(key, function);
			}
		}

		#endregion

	}

	class StateMachine {
		// delegate definitions
		public delegate void UpdateState(float delta);
		public delegate void ChangeState(int fromtostate);
		
		// dictionaries that store the functions per state
		private Dictionary<int, UpdateState> updateDel;
		private Dictionary<int, ChangeState> enterDel;
		private Dictionary<int, ChangeState> exitDel;

		// state
		private int LastState;
		public int State { get; private set; }

		public StateMachine() {
			//construct delegate dictionaries
			updateDel = new Dictionary<int, UpdateState>();
			enterDel = new Dictionary<int, ChangeState>();
			exitDel = new Dictionary<int, ChangeState>();
		}

		public void Update(float delta) {
			if (updateDel.Count > 0 && updateDel.ContainsKey(State)) {
				updateDel[State].Invoke(delta);
			}

			if (!State.Equals(LastState)) {
				if (exitDel.Count > 0 && exitDel.ContainsKey(LastState)) {
					exitDel[LastState].Invoke(State);
				}
				if (enterDel.Count > 0 && enterDel.ContainsKey(State)) {
					enterDel[State].Invoke(LastState);
				}
				LastState = State;
			}
		}
		
		public void SetState(int newstate) {
			State = newstate;
		}

		#region delegates setters

		public void AddEntry(int key, ChangeState function) {
			if (enterDel.ContainsKey(key)) {
				enterDel[key] = function;
			} else {
				enterDel.Add(key, function);
			}
		}

		public void AddUpdate(int key, UpdateState function) {
			if (updateDel.ContainsKey(key)) {
				updateDel[key] = function;
			} else {
				updateDel.Add(key, function);
			}
		}

		public void AddExit(int key, ChangeState function) {
			if (exitDel.ContainsKey(key)) {
				exitDel[key] = function;
			} else {
				exitDel.Add(key, function);
			}
		}
		
		#endregion

	}
}
