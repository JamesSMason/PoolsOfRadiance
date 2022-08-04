using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] private Image proficiencyImage = null;
    [SerializeField] private TextMeshProUGUI skillScoreText = null;
    [SerializeField] private TextMeshProUGUI skillNameText = null;

    public void SetProficiencyImage(Sprite proficiencyImage)
    {
        this.proficiencyImage.sprite = proficiencyImage;
    }

    public void SetSkillScore(int value)
    {
        skillScoreText.text = value.ToString();
    }

    public void SetSkillName(string skillName)
    {
        skillNameText.text = skillName;
    }
}