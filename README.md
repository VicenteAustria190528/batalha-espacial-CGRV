# Batalha Espacial

Projeto prático da disciplina **Computação Gráfica e Realidade Virtual** (2025/2).

Jogo de batalha espacial desenvolvido em Unity, no qual o jogador controla uma nave dentro de um corredor espacial limitado, desviando de asteroides e destruindo naves inimigas dentro de um tempo pré-determinado. O objetivo é atingir um número mínimo de inimigos destruídos e alcançar o fim da área de voo sem colidir.

## Integrantes

* Pablo Jung – 199736
* Gabriel Castanho da Rosa – 199046
* Vicente Austria – 190528

## Links do Projeto

* Download do executável do jogo: **https://drive.google.com/file/d/17SWmuyeOM0sR7xx20AhX2-L7IpV1W5VH/view?usp=drive_link**
* Vídeo demonstrativo da solução: **https://drive.google.com/file/d/13P4te5GXPTUiYNNJZ01StgPwBBe8pKga/view?usp=drive_link**
* Repositório GitHub: **https://github.com/VicenteAustria190528/batalha-espacial-CGRV**
* Repositório Drive: **https://drive.google.com/drive/u/3/folders/1VN_hMdXFHJfGaoutJ7Ema0tojAcODLaM**

(Substituir os placeholders acima pelos links finais antes da entrega.)

## Descrição do Jogo

O jogo "Batalha Espacial" coloca o jogador no comando de uma nave em um cenário tridimensional de espaço confinado. A nave percorre um corredor espacial com extensão suficiente para representar o deslocamento até o objetivo final. Durante o trajeto, o jogador deve:

* Destruir um número mínimo de naves inimigas e asteroides usando projéteis.
* Desviar de obstáculos para evitar colisões.
* Gerenciar aceleração, desaceleração e movimentos verticais e horizontais.
* Concluir a fase dentro de um tempo limite.

Ao terminar o percurso, o jogo verifica se o jogador atingiu o mínimo de inimigos destruídos e chegou ao destino a tempo. Se os requisitos forem cumpridos, o jogador vence a batalha; caso contrário, a missão é considerada fracassada.

## Mecânicas Principais

* Área de voo limitada, com início e fim bem definidos.
* Nave do jogador com aceleração constante, podendo aumentar ou reduzir a velocidade.
* Naves inimigas em movimento, com trajetórias variadas dentro da área.
* Asteroides posicionados como obstáculos a serem destruídos ou evitados.
* Sistema de projéteis que são disparados na direção do movimento da nave.
* Detecção de colisão entre nave, inimigos, asteroides e projéteis.
* Contador de tempo da missão e contagem de inimigos destruídos.

## Modos de Jogo e Dificuldade

O jogo conta com, no mínimo, dois níveis de dificuldade, selecionáveis a partir do menu principal. As diferenças entre os níveis podem incluir:

* Quantidade de naves inimigas a serem abatidas.
* Velocidade das naves inimigas.
* Quantidade de asteroides presentes no cenário.
* Tempo disponível para concluir a fase.

O objetivo em qualquer dificuldade é chegar ao fim do percurso atendendo aos requisitos de destruição mínima de inimigos dentro do tempo limite.

## Controles

Os controles foram pensados para teclado e mouse, com foco em agilidade na movimentação e no disparo:

* **W, A, S, D**: movimentação da nave (controle direcional no espaço).
* **Shift (esquerdo)**: sobe com um *boost* (acelera a nave para cima).
* **Ctrl (esquerdo)**: desacelera a nave e realiza movimento de descida.
* **Espaço**: dispara projéteis na direção da nave.
* **Botão esquerdo do mouse**: também dispara projéteis.
* **Ctrl + R**: reinicia a fase atual.
* **Ctrl + M**: retorna ao menu principal.

Outros atalhos adicionais podem ter sido implementados conforme a evolução do projeto, mas os acima são os controles principais usados na jogabilidade.

## Menu Principal e Fluxo de Navegação

O menu principal disponibiliza as seguintes opções:

* **Iniciar jogo**: começa a partida no nível de dificuldade selecionado.
* **Selecionar dificuldade**: alterna entre os níveis disponíveis (por exemplo, Fácil e Difícil).
* **Ranking**: exibe um ranking simples com as melhores pontuações/tempos obtidos pelos jogadores.
* **Sobre o jogo**: apresenta um resumo do projeto, contexto da disciplina e créditos da equipe.

A partir do menu principal, o jogador inicia a missão, joga a fase e, ao término (vitória ou derrota), pode reiniciar ou retornar ao menu para escolher outra dificuldade.

## Tecnologias Utilizadas

* **Game engine**: Unity.
* **Linguagem de programação**: C#.
* **Plataforma alvo**: PC (Windows).
* **Ferramentas adicionais**: editor de código integrado à Unity e ferramentas de modelagem/edição de assets conforme a necessidade.

## Estrutura do Repositório

Estrutura geral sugerida do projeto no GitHub:

* `Assets/`

  * `Scenes/` – cenas principais do jogo (menu, fase, etc.).
  * `Scripts/` – scripts C# responsáveis pela lógica de jogo (movimentação, tiros, inimigos, HUD, etc.).
  * `Models/` – modelos 3D das naves, asteroides e demais objetos de cenário.
  * `Audio/` – efeitos sonoros de tiros, explosões, trilha de fundo e demais sons do jogo.
  * `Prefabs/` – prefabs reutilizáveis de naves, projéteis, inimigos, asteroides e elementos de UI.
* `ProjectSettings/` – configurações do projeto Unity.
* `README.md` – documentação principal do projeto (este arquivo).

A estrutura pode sofrer pequenas variações, mas o foco do repositório é conter apenas o código-fonte e os assets necessários para abrir o projeto na Unity.

## Como Executar o Jogo (Executável)

1. Acessar o link de download do executável.
2. Baixar o arquivo compactado ou instalador disponibilizado.
3. Extrair o conteúdo (se for `.zip`).
4. Executar o arquivo `.exe` do jogo.
5. No menu inicial, selecionar a dificuldade desejada e clicar em iniciar.

## Como Rodar a Partir do Código-Fonte

1. Clonar este repositório:

   ```bash
   git clone https://github.com/VicenteAustria190528/batalha-espacial-CGRV.git
   ```

2. Abrir o projeto na Unity (versão recomendada: 2022.x ou superior).

3. Carregar a cena principal do menu na pasta `Assets/Scenes/`.

4. Clicar em **Play** dentro da Unity para testar o jogo em modo de edição ou gerar um novo build pelo menu **File > Build Settings**.

## Requisitos de Sistema (estimados)

* Sistema operacional: Windows 10 ou superior.
* Processador: dual-core ou superior.
* Memória RAM: 4 GB ou mais.
* Placa de vídeo: compatível com DirectX 11.
* Espaço em disco: ~500 MB livres.

## Status do Projeto

Este projeto foi desenvolvido como trabalho prático da disciplina de Computação Gráfica e Realidade Virtual, atendendo aos requisitos de:

* Uso de game engine (Unity) para criação de jogo de batalha espacial.
* Implementação de área de voo limitada, navegação da câmera, detecção de colisões e sistema de tiros.
* Criação de menu principal com níveis de dificuldade e ranking.
* Inclusão de efeitos visuais e sonoros.
* Geração de vídeo demonstrativo da solução.

Quaisquer melhorias futuras podem incluir novas variações de inimigos, ajustes de balanceamento, novos efeitos gráficos, suporte a outros dispositivos ou até extensões para Realidade Virtual/Realidade Aumentada.
