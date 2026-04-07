# Benchmark ECS vs MonoBehaviour

Este projeto é um teste de estresse (benchmark) criado na Unity para comparar diretamente a performance e a arquitetura do **Unity DOTS (Data-Oriented Technology Stack / ECS)** contra a abordagem tradicional baseada em **MonoBehaviours**.

## 📌 Objetivo do Projeto
Demonstrar na prática os ganhos massivos de desempenho ao utilizar a arquitetura Orientada a Dados da Unity, especialmente utilizando o sistema de Jobs e o Burst Compiler, quando comparado com as chamadas de `Update()` padrão do MonoBehaviour na Main Thread da CPU.

## 🚀 Como a comparação funciona?
O usuário pode inserir uma quantia desejada de entidades a serem testadas (através de um campo de texto no UI) e iniciar o teste em duas modalidades:
1. **Classic MonoBehaviour:** Instancia *GameObjects* clássicos. Cada inimigo/objeto possui um script `MonoEnemyWander` anexado que calcula a movimentação aleatória e a rotação no próprio método `Update()`.
2. **Unity ECS / DOTS:** Instancia *Entities* puras (usando `SpawnCommand`). O posicionamento e movimentação são processados de forma assíncrona por todas as *threads* da CPU graças a `IJobEntity` (`WanderJob`) e otimizados pelo **Burst Compiler**. 

O projeto conta também com um contador de **FPS** integrado para evidenciar a enorme diferença de quadros por segundo conforme a contagem de objetos chega aos milhares ou dezenas de milhares.

## 🛠️ Tecnologias e Pacotes Utilizados
- **Unity Engine**
- **Entities Package** (Unity ECS)
- **Burst Compiler**
- **C# Job System**
- **Unity Mathematics**
- **TextMeshPro** (Para exibir configurações do teste e FPS)

## 📁 Estrutura dos Scripts (Destaques)
- `MonoStressManager.cs` / `ECSStressManager.cs`: Gerenciadores responsáveis pela lógica de instanciamento de objetos.
- `MonoEnemyWander.cs`: Script padrão (Orientado a Objeto) que move os obstáculos clássicos.
- `WanderSystem.cs` e `WanderAuthoring.cs`: Implementação em ECS de um `ISystem` contendo Jobs assíncronos (`IJobEntity`) compilados em Burst que movem as entidades pelo espaço sem impactar o render principal.

## 🎮 Como Testar
1. Baixe o repositório e abra-o com uma versão compatível do Unity Editor.
2. Abra a cena principal encontrada na pasta de Cenas.
3. Clique em **Play**.
4. Insira um número no campo de texto (ex: `1000`, `5000`, `20000`).
5. Clique nos botões providenciados para iniciar o estresse em Mono ou em ECS e observe os quadros por segundo (FPS).
