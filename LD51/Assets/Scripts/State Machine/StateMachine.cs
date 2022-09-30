using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour {
  public State State;

  public void ChangeState(State newState) {
    if (newState == null) return;
    StopAllCoroutines();
    StartCoroutine(ChangeStateRoutine(newState));
    //if (State != null) State.Exit();
    //State = newState;
    //State.Machine = this;
    //State.Enter();
    //StartCoroutine(State.Execute());
  }

  public IEnumerator ChangeStateRoutine(State newState) {
    yield return null;
    if (State != null) State.Exit();
    State = newState;
    State.Machine = this;
    State.Enter();
    StartCoroutine(State.Execute());
  }
}