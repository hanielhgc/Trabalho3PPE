# Trabalho3PPE


1. É dado o dataset X, uma matriz de valores de dimensão 14.400 x 52. Cada coluna de X
será denotada por Xi, i = 1 ... 52.
(a) Assumindo a hipótese de que os dados são gaussianos, calcule o erro absoluto médio
(EAM) de estimar cada coluna Xi utilizando o valor esperado condicional dado o valor da
coluna Xj, E[Xi/Xj = xj], para todo j (Fórmula no Teorema 7.16). Apresente a matriz 52 x 52
com os erros absolutos médios.
(b) Utilizando os resultados do item (a) identifique, para cada coluna Xi, as duas colunas Xj e
Xk de menor erro absoluto médio (EAM). Então, estimar cada Xi utilizando o valor esperado
condicional de Xi dados Xj e Xk. Apresentar o resultado como uma matriz 52 x 5 com cada
linha sendo [id-Xi, EAM, id-Xj, valor-Xj, id-Xk, valor-Xk].
E[Xi/Xj=xj,Xk=xk] = mi + Sab.Sbb-1(Xb - mb) onde Xb = [Xj, Xk].


•	Observações e comentários:
O programa foi feito na linguagem C#. Retorna a matriz MEAM para questão 1 e indica onde houveram outliers para a questão 2. Por ser muito extenso e já conhecido, será omitido deste relatório o arquivo de entrada. Para facilitar a organização, foi criado um projeto para cada uma das questões o que implicou em dois sets de códigos.

No arquivo excel, encontram-se respectivamente a matriz MEAM da questão 1, o dataset original, o dataset após o tratamento dos outliers e a comparação da coluna 14 demonstrada graficamente. 


•	Arquivos de entrada e saída:
o	Saída no log (questão 1)
*Verificar o documento Excel para visualizar a matriz MEAM



•	Estatísticas (questão 2):
Após a correção dos outliers, pode-se perceber na coluna 14 a diferença no gráfico.
