# Bill Billiards

Protótipo de jogo de bilhar desenvolvido como trabalho acadêmico para a disciplina de **Interação Humano e Computador (IHC)** do **Centro Universitário IBMR**.

## Sobre o projeto

O Bill Billiards explora a interação do jogador com uma mesa de bilhar em um ambiente 3D. O projeto foi desenvolvido com foco na experiência de uso, na clareza dos controles e na resposta visual e física às ações do jogador.

Entre os elementos implementados estão:

- menu principal com as opções **Jogar** e **Sair**;
- movimentação da bola controlada pelo jogador;
- câmera acompanhando a bola;
- rotação visual da bola durante o movimento;
- impulso para representar a tacada;
- mesa, bolas numeradas e ambiente 3D;
- iluminação e interface desenvolvidas para a experiência de jogo.

## Objetivos de IHC

Este projeto busca aplicar conceitos de Interação Humano e Computador, incluindo:

- tornar os controles fáceis de compreender;
- fornecer resposta visual para as ações realizadas;
- manter consistência entre os elementos da interface;
- facilitar a navegação entre o menu e a cena do jogo;
- criar uma interação simples e intuitiva em um ambiente tridimensional.

## Tecnologias utilizadas

- **Unity 6000.3.8f1**
- **C#**
- **Universal Render Pipeline (URP)**
- **Unity Input System**
- **Cinemachine**
- **TextMesh Pro**
- **Git/GitHub**

## Como executar

1. Instale o Unity Hub e o Unity Editor na versão `6000.3.8f1`.
2. Clone este repositório:

   ```bash
   git clone https://github.com/ThiagoAtella/Bill-Billiards.git
   ```

3. Abra o projeto pelo Unity Hub.
4. Abra a cena `Assets/Scenes/MainMenu.unity`.
5. Pressione **Play** para iniciar a aplicação.

## Controles

| Ação | Teclado |
| --- | --- |
| Movimentar | `W`, `A`, `S`, `D` ou setas |
| Câmera | Movimento do mouse |
| Tacada/impulso | `Espaço` |
| Interagir | `E` |
| Correr | `Shift` |

O projeto também possui configurações para controles compatíveis com gamepad por meio do Unity Input System.

## Organização principal

```text
Assets/
├── Scenes/
│   ├── MainMenu.unity
│   └── SampleScene.unity
├── Scripts/
│   ├── BallPhysicsController.cs
│   ├── BallRolling.cs
│   ├── BallCameraTarget.cs
│   └── BolaAlvo.cs
├── Eight-ball Rack/
└── menu/
```

## Contexto acadêmico

**Instituição:** Centro Universitário IBMR

**Disciplina:** Interação Humano e Computador (IHC)

**Projeto:** A3 — Bill Billiards

## Licença

Este projeto foi desenvolvido para fins acadêmicos.
