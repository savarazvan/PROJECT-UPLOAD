﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNG
{ 
    //Avem un tablou unidimensional de 256 de elemente generate la intamplare
    static readonly int[] table = new int[256] {42,
                36,
                191,
                133,
                226,
                109,
                215,
                175,
                83,
                145,
                74,
                242,
                242,
                188,
                234,
                236,
                180,
                167,
                220,
                61,
                136,
                13,
                63,
                154,
                37,
                95,
                14,
                29,
                7,
                184,
                72,
                223,
                180,
                19,
                78,
                201,
                68,
                188,
                140,
                167,
                32,
                4,
                91,
                126,
                10,
                57,
                38,
                32,
                94,
                213,
                204,
                253,
                151,
                246,
                70,
                60,
                20,
                14,
                138,
                11,
                29,
                220,
                175,
                51,
                33,
                155,
                81,
                239,
                65,
                121,
                55,
                254,
                19,
                74,
                51,
                247,
                159,
                126,
                74,
                221,
                174,
                80,
                21,
                243,
                69,
                65,
                103,
                209,
                108,
                197,
                49,
                184,
                51,
                60,
                162,
                35,
                247,
                35,
                146,
                158,
                226,
                140,
                32,
                219,
                177,
                203,
                154,
                3,
                186,
                115,
                158,
                74,
                45,
                129,
                127,
                198,
                154,
                214,
                234,
                129,
                179,
                235,
                202,
                205,
                84,
                192,
                104,
                215,
                192,
                21,
                215,
                127,
                46,
                221,
                143,
                103,
                132,
                240,
                88,
                74,
                98,
                256,
                106,
                144,
                98,
                206,
                210,
                31,
                158,
                157,
                23,
                115,
                115,
                231,
                30,
                241,
                133,
                80,
                75,
                120,
                3,
                216,
                233,
                58,
                45,
                84,
                204,
                202,
                19,
                31,
                52,
                117,
                159,
                13,
                245,
                214,
                213,
                160,
                213,
                165,
                90,
                127,
                54,
                208,
                51,
                35,
                245,
                205,
                208,
                212,
                145,
                46,
                73,
                212,
                144,
                118,
                231,
                218,
                30,
                43,
                230,
                193,
                248,
                44,
                121,
                130,
                136,
                69,
                15,
                96,
                81,
                1,
                213,
                98,
                142,
                191,
                124,
                6,
                22,
                8,
                60,
                52,
                131,
                32,
                25,
                113,
                147,
                219,
                101,
                85,
                207,
                178,
                134,
                63,
                106,
                22,
                249,
                71,
                107,
                5,
                151,
                116,
                15,
                218,
                23,
                48,
                104,
                105,
                213,
                248,
                75,
                75,
                209,
                88,
                105,
                251
                };
    private static int i;
    void Start()
    {
        i = 0; //variabila i va determina indexingul tabloului si va incepe pe pozitia 0
    }
    public static int Rng()//la fiecare apel, indexul va creste cu pozitie
    {
        i++;
        if (i >table.Length-1) i = 0; //daca i ajunge la capatul tabloului, se va intoarce la pozitia 0
        return table[i];
    }
}
