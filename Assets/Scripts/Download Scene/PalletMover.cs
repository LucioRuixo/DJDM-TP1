using UnityEngine;

public class PalletMover : ManejoPallets
{
    bool segundoCompleto = false;

    public ManejoPallets Desde, Hasta;

#if UNITY_STANDALONE
    public MoveType miInput;
    public enum MoveType
    {
        WASD,
        Arrows
    }
#endif

#if UNITY_ANDROID
    public InputController playerInput;
#endif

    void Update()
    {
#if UNITY_STANDALONE
        switch (miInput)
        {
            case MoveType.WASD:
                if (!HasPallets() && Desde.HasPallets() && Input.GetKeyDown(KeyCode.A))
                    PrimerPaso();
                if (HasPallets() && Input.GetKeyDown(KeyCode.S))
                    SegundoPaso();
                if (segundoCompleto && HasPallets() && Input.GetKeyDown(KeyCode.D))
                    TercerPaso();
                break;
            case MoveType.Arrows:
                if (!HasPallets() && Desde.HasPallets() && Input.GetKeyDown(KeyCode.LeftArrow))
                    PrimerPaso();
                if (HasPallets() && Input.GetKeyDown(KeyCode.DownArrow))
                    SegundoPaso();
                if (segundoCompleto && HasPallets() && Input.GetKeyDown(KeyCode.RightArrow))
                    TercerPaso();
                break;
            default:
                break;
        }
#endif

#if UNITY_ANDROID
        if (!playerInput.joystick) return;

        if (!HasPallets() && Desde.HasPallets() && playerInput.joystick.Horizontal < 0f)
            PrimerPaso();
        if (HasPallets() && playerInput.joystick.Vertical < 0f)
            SegundoPaso();
        if (segundoCompleto && HasPallets() && playerInput.joystick.Horizontal > 0f)
            TercerPaso();
#endif
    }

    void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso()
    {
        Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (HasPallets() && receptor.Recibir(Pallets[0]))
            Pallets.RemoveAt(0);
    }

    public override bool Recibir(Pallet pallet)
    {
        if (!HasPallets())
        {
            pallet.Portador = gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}