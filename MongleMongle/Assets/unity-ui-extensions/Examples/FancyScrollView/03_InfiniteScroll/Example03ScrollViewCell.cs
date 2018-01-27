﻿namespace UnityEngine.UI.Extensions.Examples
{
    public class Example03ScrollViewCell
        : FancyScrollViewCell<Example03CellDto, Example03ScrollViewContext>
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        Text message;
        [SerializeField]
        Image image;
        [SerializeField]
        Button button;

        readonly int scrollTriggerHash = Animator.StringToHash("scroll");
        Example03ScrollViewContext context;

        void Start()
        {
            var rectTransform = transform as RectTransform;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchoredPosition3D = Vector3.zero;
            UpdatePosition(0);

            button.onClick.AddListener(OnPressedCell);
        }

        /// <summary>
        /// コンテキストを設定します
        /// </summary>
        /// <param name="context"></param>
        public override void SetContext(Example03ScrollViewContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// セルの内容を更新します
        /// </summary>
        /// <param name="itemData"></param>
        public override void UpdateContent(Example03CellDto itemData)
        {
            message.text = itemData.Message;

            if (context != null)
            {
                var isSelected = context.SelectedIndex == DataIndex;
                image.color = isSelected
                    ? new Color32(0, 255, 255, 255)
                    : new Color32(255, 255, 255, 77);
            }
        }

        /// <summary>
        /// セルの位置を更新します
        /// </summary>
        /// <param name="position"></param>
        public override void UpdatePosition(float position)
        {
            int nPos = (int)(Mathf.Floor(position * 1000) / 118);

            switch(nPos)
            {
                case 2:
                    transform.SetSiblingIndex(transform.parent.childCount - 4);
                    break;
                case 3:
                    transform.SetSiblingIndex(transform.parent.childCount - 2);
                    break;
                case 4:
                    transform.SetAsLastSibling();
                    break;
                case 5:
                    transform.SetSiblingIndex(transform.parent.childCount - 3);
                    break;
                case 6:
                    transform.SetSiblingIndex(transform.parent.childCount - 5);
                    break;
            }

            animator.Play(scrollTriggerHash, -1, position);
            animator.speed = 0;
        }

        public void OnPressedCell()
        {
            if (context != null)
            {
                context.OnPressedCell(this);
            }
        }
    }
}
