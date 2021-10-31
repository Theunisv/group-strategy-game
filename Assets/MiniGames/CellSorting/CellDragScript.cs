using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGames.CellSorting
{
    public class CellDragScript : EventTrigger {

        private bool dragging;
        public bool inWinPosition = false;

        public void Update() {
            if (dragging && !inWinPosition) {
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        public override void OnPointerDown(PointerEventData eventData) {
            dragging = true;
        }

        public override void OnPointerUp(PointerEventData eventData) {
            dragging = false;
        }
    }
}

