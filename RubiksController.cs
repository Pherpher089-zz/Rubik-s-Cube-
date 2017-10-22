using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksController : RubiksElement {

    // Handles the ball hit event
    public virtual void OnNotification(string p_event_path, Object p_target, params object[] p_data){ }
}
