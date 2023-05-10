using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryInView : VictoryLanding
{
    protected override void ControlBySelf()
    {
        if (Shuttle.shuttle == null)
        {
            return;
        }
        image.fillAmount = victoryTimer / time;
        Shuttle.shuttle.Winprogress = image.fillAmount;
        Vector2 shuttlePos = Camera.main.WorldToScreenPoint(Shuttle.shuttle.gameObject.transform.position);
        if (shuttlePos.x < Screen.width && shuttlePos.x > 0
            && shuttlePos.y < Screen.height && shuttlePos.y > 0)
        {
            victoryTimer += Time.deltaTime * Shuttle.timeScale * Shuttle.shuttle.speedRate;
            if (victoryTimer > time)
            {
                image.fillAmount = 0;
                End.EndGame(true);
            }
        }
        else
        {

            victoryTimer = 0;
        }
    }
}
