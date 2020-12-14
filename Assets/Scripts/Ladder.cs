using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
   private enum LadderParts { Complete, Bottom, Top }

    [SerializeField]

    LadderParts Parts = LadderParts.Complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            switch (Parts)
            {
                case LadderParts.Complete:

                    player.CanClimb = true;

                    player.ladder = this;

                    break;

                case LadderParts.Bottom:

                    player.BottomLadder = true;

                    break;

                case LadderParts.Top:

                    player.TopLadder = true;

                    break;

                default:

                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            switch (Parts)
            {
                case LadderParts.Complete:

                    player.CanClimb = false;

                    break;

                case LadderParts.Bottom:

                    player.BottomLadder = false;

                    break;

                case LadderParts.Top:

                    player.TopLadder = false;

                    break;

                default:

                    break;
            }
        }
    }
}
