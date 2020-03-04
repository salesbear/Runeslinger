using System.Collections;
public interface IPanel
{
    IEnumerator AnimateIn();
    IEnumerator AnimateOut();
    bool InScene();
    bool IsAnimating();
}
