using System.Reflection;

namespace __Forjerum.Database
{
    class Manual
    {
        //*********************************************************************************************
        // loadShopsRud / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadShopsRud()
        {
            //Manual, no editor, sorry :/
            ShopStruct.shop[1].map = 3;
            ShopStruct.shop[1].x = 9;
            ShopStruct.shop[1].y = 7;
            ShopStruct.shop[1].item_count = 2;

            ShopStruct.shopitem[1, 1].type = 1;
            ShopStruct.shopitem[1, 1].num = 1;
            ShopStruct.shopitem[1, 1].value = 1;
            ShopStruct.shopitem[1, 1].refin = 0;
            ShopStruct.shopitem[1, 1].price = 75;

            ShopStruct.shopitem[1, 2].type = 1;
            ShopStruct.shopitem[1, 2].num = 2;
            ShopStruct.shopitem[1, 2].value = 1;
            ShopStruct.shopitem[1, 2].refin = 0;
            ShopStruct.shopitem[1, 2].price = 75;

            //Manual, no editor, sorry :/
            ShopStruct.shop[2].map = 132;
            ShopStruct.shop[2].x = 13;
            ShopStruct.shop[2].y = 7;
            ShopStruct.shop[2].item_count = 1;

            ShopStruct.shopitem[2, 1].type = 1;
            ShopStruct.shopitem[2, 1].num = 50;
            ShopStruct.shopitem[2, 1].value = 1;
            ShopStruct.shopitem[2, 1].refin = 0;
            ShopStruct.shopitem[2, 1].price = 25;

            //Manual, no editor, sorry :/
            ShopStruct.shop[3].map = 136;
            ShopStruct.shop[3].x = 12;
            ShopStruct.shop[3].y = 10;
            ShopStruct.shop[3].item_count = 1;

            ShopStruct.shopitem[3, 1].type = 2;
            ShopStruct.shopitem[3, 1].num = 28;
            ShopStruct.shopitem[3, 1].value = 1;
            ShopStruct.shopitem[3, 1].refin = 0;
            ShopStruct.shopitem[3, 1].price = 1;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[4].map = 77;
            ShopStruct.shop[4].x = 13;
            ShopStruct.shop[4].y = 3;
            ShopStruct.shop[4].item_count = 1;

            ShopStruct.shopitem[4, 1].type = 1;
            ShopStruct.shopitem[4, 1].num = 5;
            ShopStruct.shopitem[4, 1].value = 1;
            ShopStruct.shopitem[4, 1].refin = 0;
            ShopStruct.shopitem[4, 1].price = 100;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[5].map = 148;
            ShopStruct.shop[5].x = 9;
            ShopStruct.shop[5].y = 7;
            ShopStruct.shop[5].item_count = 1;

            ShopStruct.shopitem[5, 1].type = 2;
            ShopStruct.shopitem[5, 1].num = 31;
            ShopStruct.shopitem[5, 1].value = 1;
            ShopStruct.shopitem[5, 1].refin = 0;
            ShopStruct.shopitem[5, 1].price = 1;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[6].map = 149;
            ShopStruct.shop[6].x = 10;
            ShopStruct.shop[6].y = 6;
            ShopStruct.shop[6].item_count = 1;

            ShopStruct.shopitem[6, 1].type = 1;
            ShopStruct.shopitem[6, 1].num = 47;
            ShopStruct.shopitem[6, 1].value = 1;
            ShopStruct.shopitem[6, 1].refin = 0;
            ShopStruct.shopitem[6, 1].price = 50;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[7].map = 0;
            ShopStruct.shop[7].x = 0;
            ShopStruct.shop[7].y = 0;
            ShopStruct.shop[7].item_count = 3;

            ShopStruct.shopitem[7, 1].type = 1;
            ShopStruct.shopitem[7, 1].num = 68;
            ShopStruct.shopitem[7, 1].value = 1;
            ShopStruct.shopitem[7, 1].refin = 0;
            ShopStruct.shopitem[7, 1].price = 5;// 225;

            ShopStruct.shopitem[7, 2].type = 1;
            ShopStruct.shopitem[7, 2].num = 70;
            ShopStruct.shopitem[7, 2].value = 1;
            ShopStruct.shopitem[7, 2].refin = 0;
            ShopStruct.shopitem[7, 2].price = 10;// 225;

            ShopStruct.shopitem[7, 3].type = 1;
            ShopStruct.shopitem[7, 3].num = 71;
            ShopStruct.shopitem[7, 3].value = 1;
            ShopStruct.shopitem[7, 3].refin = 0;
            ShopStruct.shopitem[7, 3].price = 10;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[8].map = 13;
            ShopStruct.shop[8].x = 5;
            ShopStruct.shop[8].y = 7;
            ShopStruct.shop[8].item_count = 1;

            ShopStruct.shopitem[8, 1].type = 1;
            ShopStruct.shopitem[8, 1].num = 69;
            ShopStruct.shopitem[8, 1].value = 1;
            ShopStruct.shopitem[8, 1].refin = 0;
            ShopStruct.shopitem[8, 1].price = 500;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[9].map = 167;
            ShopStruct.shop[9].x = 11;
            ShopStruct.shop[9].y = 6;
            ShopStruct.shop[9].item_count = 11;

            ShopStruct.shopitem[9, 1].type = 2;
            ShopStruct.shopitem[9, 1].num = 2;
            ShopStruct.shopitem[9, 1].value = 1;
            ShopStruct.shopitem[9, 1].refin = 0;
            ShopStruct.shopitem[9, 1].price = 209;// 225;

            ShopStruct.shopitem[9, 2].type = 2;
            ShopStruct.shopitem[9, 2].num = 3;
            ShopStruct.shopitem[9, 2].value = 1;
            ShopStruct.shopitem[9, 2].refin = 0;
            ShopStruct.shopitem[9, 2].price = 477;// 225;

            ShopStruct.shopitem[9, 3].type = 2;
            ShopStruct.shopitem[9, 3].num = 4;
            ShopStruct.shopitem[9, 3].value = 1;
            ShopStruct.shopitem[9, 3].refin = 0;
            ShopStruct.shopitem[9, 3].price = 990;// 225;

            ShopStruct.shopitem[9, 4].type = 2;
            ShopStruct.shopitem[9, 4].num = 5;
            ShopStruct.shopitem[9, 4].value = 1;
            ShopStruct.shopitem[9, 4].refin = 0;
            ShopStruct.shopitem[9, 4].price = 1650;// 225;

            ShopStruct.shopitem[9, 5].type = 2;
            ShopStruct.shopitem[9, 5].num = 6;
            ShopStruct.shopitem[9, 5].value = 1;
            ShopStruct.shopitem[9, 5].refin = 0;
            ShopStruct.shopitem[9, 5].price = 3350;// 225;

            ShopStruct.shopitem[9, 6].type = 2;
            ShopStruct.shopitem[9, 6].num = 7;
            ShopStruct.shopitem[9, 6].value = 1;
            ShopStruct.shopitem[9, 6].refin = 0;
            ShopStruct.shopitem[9, 6].price = 5450;// 225;

            ShopStruct.shopitem[9, 7].type = 2;
            ShopStruct.shopitem[9, 7].num = 8;
            ShopStruct.shopitem[9, 7].value = 1;
            ShopStruct.shopitem[9, 7].refin = 0;
            ShopStruct.shopitem[9, 7].price = 8220;// 225;

            ShopStruct.shopitem[9, 8].type = 2;
            ShopStruct.shopitem[9, 8].num = 8;
            ShopStruct.shopitem[9, 8].value = 1;
            ShopStruct.shopitem[9, 8].refin = 0;
            ShopStruct.shopitem[9, 8].price = 10130;// 225;

            ShopStruct.shopitem[9, 9].type = 2;
            ShopStruct.shopitem[9, 9].num = 9;
            ShopStruct.shopitem[9, 9].value = 1;
            ShopStruct.shopitem[9, 9].refin = 0;
            ShopStruct.shopitem[9, 9].price = 13630;// 225;

            ShopStruct.shopitem[9, 10].type = 2;
            ShopStruct.shopitem[9, 10].num = 10;
            ShopStruct.shopitem[9, 10].value = 1;
            ShopStruct.shopitem[9, 10].refin = 0;
            ShopStruct.shopitem[9, 10].price = 17780;// 225;

            ShopStruct.shopitem[9, 11].type = 2;
            ShopStruct.shopitem[9, 11].num = 11;
            ShopStruct.shopitem[9, 11].value = 1;
            ShopStruct.shopitem[9, 11].refin = 0;
            ShopStruct.shopitem[9, 11].price = 23180;// 225;


            //Manual, no editor, sorry :/
            ShopStruct.shop[10].map = 168;
            ShopStruct.shop[10].x = 11;
            ShopStruct.shop[10].y = 6;
            ShopStruct.shop[10].item_count = 12;

            ShopStruct.shopitem[10, 1].type = 3;
            ShopStruct.shopitem[10, 1].num = 2;
            ShopStruct.shopitem[10, 1].value = 1;
            ShopStruct.shopitem[10, 1].refin = 0;
            ShopStruct.shopitem[10, 1].price = 189;// 225;

            ShopStruct.shopitem[10, 2].type = 3;
            ShopStruct.shopitem[10, 2].num = 3;
            ShopStruct.shopitem[10, 2].value = 1;
            ShopStruct.shopitem[10, 2].refin = 0;
            ShopStruct.shopitem[10, 2].price = 389;// 225;

            ShopStruct.shopitem[10, 3].type = 3;
            ShopStruct.shopitem[10, 3].num = 4;
            ShopStruct.shopitem[10, 3].value = 1;
            ShopStruct.shopitem[10, 3].refin = 0;
            ShopStruct.shopitem[10, 3].price = 756;// 225;

            ShopStruct.shopitem[10, 4].type = 3;
            ShopStruct.shopitem[10, 4].num = 5;
            ShopStruct.shopitem[10, 4].value = 1;
            ShopStruct.shopitem[10, 4].refin = 0;
            ShopStruct.shopitem[10, 4].price = 1229;// 225;

            ShopStruct.shopitem[10, 5].type = 3;
            ShopStruct.shopitem[10, 5].num = 14;
            ShopStruct.shopitem[10, 5].value = 1;
            ShopStruct.shopitem[10, 5].refin = 0;
            ShopStruct.shopitem[10, 5].price = 4663;// 225;

            ShopStruct.shopitem[10, 6].type = 3;
            ShopStruct.shopitem[10, 6].num = 15;
            ShopStruct.shopitem[10, 6].value = 1;
            ShopStruct.shopitem[10, 6].refin = 0;
            ShopStruct.shopitem[10, 6].price = 7224;// 225;

            ShopStruct.shopitem[10, 7].type = 3;
            ShopStruct.shopitem[10, 7].num = 16;
            ShopStruct.shopitem[10, 7].value = 1;
            ShopStruct.shopitem[10, 7].refin = 0;
            ShopStruct.shopitem[10, 7].price = 10101;// 225;

            ShopStruct.shopitem[10, 8].type = 3;
            ShopStruct.shopitem[10, 8].num = 17;
            ShopStruct.shopitem[10, 8].value = 1;
            ShopStruct.shopitem[10, 8].refin = 0;
            ShopStruct.shopitem[10, 8].price = 14101;// 225;

            ShopStruct.shopitem[10, 9].type = 3;
            ShopStruct.shopitem[10, 9].num = 18;
            ShopStruct.shopitem[10, 9].value = 1;
            ShopStruct.shopitem[10, 9].refin = 0;
            ShopStruct.shopitem[10, 9].price = 19243;// 225;

            ShopStruct.shopitem[10, 10].type = 3;
            ShopStruct.shopitem[10, 10].num = 22;
            ShopStruct.shopitem[10, 10].value = 1;
            ShopStruct.shopitem[10, 10].refin = 0;
            ShopStruct.shopitem[10, 10].price = 22342;// 225;

            ShopStruct.shopitem[10, 11].type = 3;
            ShopStruct.shopitem[10, 11].num = 23;
            ShopStruct.shopitem[10, 11].value = 1;
            ShopStruct.shopitem[10, 11].refin = 0;
            ShopStruct.shopitem[10, 11].price = 26347;// 225;

            ShopStruct.shopitem[10, 12].type = 3;
            ShopStruct.shopitem[10, 12].num = 24;
            ShopStruct.shopitem[10, 12].value = 1;
            ShopStruct.shopitem[10, 12].refin = 0;
            ShopStruct.shopitem[10, 12].price = 32345;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[11].map = 174;
            ShopStruct.shop[11].x = 11;
            ShopStruct.shop[11].y = 6;
            ShopStruct.shop[11].item_count = 6;

            ShopStruct.shopitem[11, 1].type = 2;
            ShopStruct.shopitem[11, 1].num = 34;
            ShopStruct.shopitem[11, 1].value = 1;
            ShopStruct.shopitem[11, 1].refin = 0;
            ShopStruct.shopitem[11, 1].price = 500;// 225;

            ShopStruct.shopitem[11, 2].type = 2;
            ShopStruct.shopitem[11, 2].num = 32;
            ShopStruct.shopitem[11, 2].value = 1;
            ShopStruct.shopitem[11, 2].refin = 0;
            ShopStruct.shopitem[11, 2].price = 1200;// 225;

            ShopStruct.shopitem[11, 3].type = 2;
            ShopStruct.shopitem[11, 3].num = 33;
            ShopStruct.shopitem[11, 3].value = 1;
            ShopStruct.shopitem[11, 3].refin = 0;
            ShopStruct.shopitem[11, 3].price = 2100;// 225;

            ShopStruct.shopitem[11, 4].type = 2;
            ShopStruct.shopitem[11, 4].num = 35;
            ShopStruct.shopitem[11, 4].value = 1;
            ShopStruct.shopitem[11, 4].refin = 0;
            ShopStruct.shopitem[11, 4].price = 5000;// 225;

            ShopStruct.shopitem[11, 5].type = 2;
            ShopStruct.shopitem[11, 5].num = 36;
            ShopStruct.shopitem[11, 5].value = 1;
            ShopStruct.shopitem[11, 5].refin = 0;
            ShopStruct.shopitem[11, 5].price = 8000;// 225;

            ShopStruct.shopitem[11, 6].type = 2;
            ShopStruct.shopitem[11, 6].num = 37;
            ShopStruct.shopitem[11, 6].value = 1;
            ShopStruct.shopitem[11, 6].refin = 0;
            ShopStruct.shopitem[11, 6].price = 14000; //225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[12].map = 175;
            ShopStruct.shop[12].x = 11;
            ShopStruct.shop[12].y = 6;
            ShopStruct.shop[12].item_count = 11;

            ShopStruct.shopitem[12, 1].type = 3;
            ShopStruct.shopitem[12, 1].num = 7;
            ShopStruct.shopitem[12, 1].value = 1;
            ShopStruct.shopitem[12, 1].refin = 0;
            ShopStruct.shopitem[12, 1].price = 300;// 225;

            ShopStruct.shopitem[12, 2].type = 3;
            ShopStruct.shopitem[12, 2].num = 8;
            ShopStruct.shopitem[12, 2].value = 1;
            ShopStruct.shopitem[12, 2].refin = 0;
            ShopStruct.shopitem[12, 2].price = 600;// 225;

            ShopStruct.shopitem[12, 3].type = 3;
            ShopStruct.shopitem[12, 3].num = 9;
            ShopStruct.shopitem[12, 3].value = 1;
            ShopStruct.shopitem[12, 3].refin = 0;
            ShopStruct.shopitem[12, 3].price = 1100;// 225;

            ShopStruct.shopitem[12, 4].type = 3;
            ShopStruct.shopitem[12, 4].num = 10;
            ShopStruct.shopitem[12, 4].value = 1;
            ShopStruct.shopitem[12, 4].refin = 0;
            ShopStruct.shopitem[12, 4].price = 1800;// 225;

            ShopStruct.shopitem[12, 5].type = 3;
            ShopStruct.shopitem[12, 5].num = 11;
            ShopStruct.shopitem[12, 5].value = 1;
            ShopStruct.shopitem[12, 5].refin = 0;
            ShopStruct.shopitem[12, 5].price = 2500;// 225;

            ShopStruct.shopitem[12, 6].type = 3;
            ShopStruct.shopitem[12, 6].num = 12;
            ShopStruct.shopitem[12, 6].value = 1;
            ShopStruct.shopitem[12, 6].refin = 0;
            ShopStruct.shopitem[12, 6].price = 4000;// 225;

            ShopStruct.shopitem[12, 7].type = 3;
            ShopStruct.shopitem[12, 7].num = 13;
            ShopStruct.shopitem[12, 7].value = 1;
            ShopStruct.shopitem[12, 7].refin = 0;
            ShopStruct.shopitem[12, 7].price = 6200;// 225;

            ShopStruct.shopitem[12, 8].type = 3;
            ShopStruct.shopitem[12, 8].num = 25;
            ShopStruct.shopitem[12, 8].value = 1;
            ShopStruct.shopitem[12, 8].refin = 0;
            ShopStruct.shopitem[12, 8].price = 9300;// 225;

            ShopStruct.shopitem[12, 9].type = 3;
            ShopStruct.shopitem[12, 9].num = 26;
            ShopStruct.shopitem[12, 9].value = 1;
            ShopStruct.shopitem[12, 9].refin = 0;
            ShopStruct.shopitem[12, 9].price = 13800;// 225;

            ShopStruct.shopitem[12, 10].type = 3;
            ShopStruct.shopitem[12, 10].num = 27;
            ShopStruct.shopitem[12, 10].value = 1;
            ShopStruct.shopitem[12, 10].refin = 0;
            ShopStruct.shopitem[12, 10].price = 17100;// 225;

            ShopStruct.shopitem[12, 11].type = 3;
            ShopStruct.shopitem[12, 11].num = 30;
            ShopStruct.shopitem[12, 11].value = 1;
            ShopStruct.shopitem[12, 11].refin = 0;
            ShopStruct.shopitem[12, 11].price = 22400;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[13].map = 170;
            ShopStruct.shop[13].x = 11;
            ShopStruct.shop[13].y = 6;
            ShopStruct.shop[13].item_count = 7;

            ShopStruct.shopitem[13, 1].type = 1;
            ShopStruct.shopitem[13, 1].num = 1;
            ShopStruct.shopitem[13, 1].value = 1;
            ShopStruct.shopitem[13, 1].refin = 0;
            ShopStruct.shopitem[13, 1].price = 75;// 225;

            ShopStruct.shopitem[13, 2].type = 1;
            ShopStruct.shopitem[13, 2].num = 2;
            ShopStruct.shopitem[13, 2].value = 1;
            ShopStruct.shopitem[13, 2].refin = 0;
            ShopStruct.shopitem[13, 2].price = 75;// 225;

            ShopStruct.shopitem[13, 3].type = 1;
            ShopStruct.shopitem[13, 3].num = 69;
            ShopStruct.shopitem[13, 3].value = 1;
            ShopStruct.shopitem[13, 3].refin = 0;
            ShopStruct.shopitem[13, 3].price = 500;// 225;

            ShopStruct.shopitem[13, 4].type = 1;
            ShopStruct.shopitem[13, 4].num = 73;
            ShopStruct.shopitem[13, 4].value = 1;
            ShopStruct.shopitem[13, 4].refin = 0;
            ShopStruct.shopitem[13, 4].price = 200;// 225;

            ShopStruct.shopitem[13, 5].type = 1;
            ShopStruct.shopitem[13, 5].num = 72;
            ShopStruct.shopitem[13, 5].value = 1;
            ShopStruct.shopitem[13, 5].refin = 0;
            ShopStruct.shopitem[13, 5].price = 700;// 225;

            ShopStruct.shopitem[13, 6].type = 1;
            ShopStruct.shopitem[13, 6].num = 74;
            ShopStruct.shopitem[13, 6].value = 1;
            ShopStruct.shopitem[13, 6].refin = 0;
            ShopStruct.shopitem[13, 6].price = 500;// 225;

            ShopStruct.shopitem[13, 7].type = 1;
            ShopStruct.shopitem[13, 7].num = 75;
            ShopStruct.shopitem[13, 7].value = 1;
            ShopStruct.shopitem[13, 7].refin = 0;
            ShopStruct.shopitem[13, 7].price = 1000;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[14].map = 169;
            ShopStruct.shop[14].x = 11;
            ShopStruct.shop[14].y = 6;
            ShopStruct.shop[14].item_count = 1;

            ShopStruct.shopitem[14, 1].type = 1;
            ShopStruct.shopitem[14, 1].num = 68;
            ShopStruct.shopitem[14, 1].value = 1;
            ShopStruct.shopitem[14, 1].refin = 0;
            ShopStruct.shopitem[14, 1].price = 100000;// 225;


            //Manual, no editor, sorry :/
            ShopStruct.shop[15].map = 219;
            ShopStruct.shop[15].x = 11;
            ShopStruct.shop[15].y = 6;
            ShopStruct.shop[15].item_count = 11;

            ShopStruct.shopitem[15, 1].type = 3;
            ShopStruct.shopitem[15, 1].num = 7;
            ShopStruct.shopitem[15, 1].value = 1;
            ShopStruct.shopitem[15, 1].refin = 0;
            ShopStruct.shopitem[15, 1].price = 300;// 225;

            ShopStruct.shopitem[15, 2].type = 3;
            ShopStruct.shopitem[15, 2].num = 8;
            ShopStruct.shopitem[15, 2].value = 1;
            ShopStruct.shopitem[15, 2].refin = 0;
            ShopStruct.shopitem[15, 2].price = 600;// 225;

            ShopStruct.shopitem[15, 3].type = 3;
            ShopStruct.shopitem[15, 3].num = 9;
            ShopStruct.shopitem[15, 3].value = 1;
            ShopStruct.shopitem[15, 3].refin = 0;
            ShopStruct.shopitem[15, 3].price = 1100;// 225;

            ShopStruct.shopitem[15, 4].type = 3;
            ShopStruct.shopitem[15, 4].num = 10;
            ShopStruct.shopitem[15, 4].value = 1;
            ShopStruct.shopitem[15, 4].refin = 0;
            ShopStruct.shopitem[15, 4].price = 1800;// 225;

            ShopStruct.shopitem[15, 5].type = 3;
            ShopStruct.shopitem[15, 5].num = 11;
            ShopStruct.shopitem[15, 5].value = 1;
            ShopStruct.shopitem[15, 5].refin = 0;
            ShopStruct.shopitem[15, 5].price = 2500;// 225;

            ShopStruct.shopitem[15, 6].type = 3;
            ShopStruct.shopitem[15, 6].num = 12;
            ShopStruct.shopitem[15, 6].value = 1;
            ShopStruct.shopitem[15, 6].refin = 0;
            ShopStruct.shopitem[15, 6].price = 4000;// 225;

            ShopStruct.shopitem[15, 7].type = 3;
            ShopStruct.shopitem[15, 7].num = 13;
            ShopStruct.shopitem[15, 7].value = 1;
            ShopStruct.shopitem[15, 7].refin = 0;
            ShopStruct.shopitem[15, 7].price = 6200;// 225;

            ShopStruct.shopitem[15, 8].type = 3;
            ShopStruct.shopitem[15, 8].num = 25;
            ShopStruct.shopitem[15, 8].value = 1;
            ShopStruct.shopitem[15, 8].refin = 0;
            ShopStruct.shopitem[15, 8].price = 9300;// 225;

            ShopStruct.shopitem[15, 9].type = 3;
            ShopStruct.shopitem[15, 9].num = 26;
            ShopStruct.shopitem[15, 9].value = 1;
            ShopStruct.shopitem[15, 9].refin = 0;
            ShopStruct.shopitem[15, 9].price = 13800;// 225;

            ShopStruct.shopitem[15, 10].type = 3;
            ShopStruct.shopitem[15, 10].num = 27;
            ShopStruct.shopitem[15, 10].value = 1;
            ShopStruct.shopitem[15, 10].refin = 0;
            ShopStruct.shopitem[15, 10].price = 17100;// 225;

            ShopStruct.shopitem[15, 11].type = 3;
            ShopStruct.shopitem[15, 11].num = 30;
            ShopStruct.shopitem[15, 11].value = 1;
            ShopStruct.shopitem[15, 11].refin = 0;
            ShopStruct.shopitem[15, 11].price = 22400;// 225;

            //Manual, no editor, sorry :/
            ShopStruct.shop[16].map = 220;
            ShopStruct.shop[16].x = 11;
            ShopStruct.shop[16].y = 6;
            ShopStruct.shop[16].item_count = 7;

            ShopStruct.shopitem[16, 1].type = 1;
            ShopStruct.shopitem[16, 1].num = 1;
            ShopStruct.shopitem[16, 1].value = 1;
            ShopStruct.shopitem[16, 1].refin = 0;
            ShopStruct.shopitem[16, 1].price = 75;// 225;

            ShopStruct.shopitem[16, 2].type = 1;
            ShopStruct.shopitem[16, 2].num = 2;
            ShopStruct.shopitem[16, 2].value = 1;
            ShopStruct.shopitem[16, 2].refin = 0;
            ShopStruct.shopitem[16, 2].price = 75;// 225;

            ShopStruct.shopitem[16, 3].type = 1;
            ShopStruct.shopitem[16, 3].num = 69;
            ShopStruct.shopitem[16, 3].value = 1;
            ShopStruct.shopitem[16, 3].refin = 0;
            ShopStruct.shopitem[16, 3].price = 500;// 225;

            ShopStruct.shopitem[16, 4].type = 1;
            ShopStruct.shopitem[16, 4].num = 73;
            ShopStruct.shopitem[16, 4].value = 1;
            ShopStruct.shopitem[16, 4].refin = 0;
            ShopStruct.shopitem[16, 4].price = 200;// 225;

            ShopStruct.shopitem[16, 5].type = 1;
            ShopStruct.shopitem[16, 5].num = 72;
            ShopStruct.shopitem[16, 5].value = 1;
            ShopStruct.shopitem[16, 5].refin = 0;
            ShopStruct.shopitem[16, 5].price = 700;// 225;

            ShopStruct.shopitem[16, 6].type = 1;
            ShopStruct.shopitem[16, 6].num = 74;
            ShopStruct.shopitem[16, 6].value = 1;
            ShopStruct.shopitem[16, 6].refin = 0;
            ShopStruct.shopitem[16, 6].price = 500;// 225;

            ShopStruct.shopitem[16, 7].type = 1;
            ShopStruct.shopitem[16, 7].num = 75;
            ShopStruct.shopitem[16, 7].value = 1;
            ShopStruct.shopitem[16, 7].refin = 0;
            ShopStruct.shopitem[16, 7].price = 1000;// 225;
        }
        //*********************************************************************************************
        // loadRecipes / Revisto pela última vez em 01/08/2016, criado por Allyson S. Bacon
        //*********************************************************************************************
        public static void loadRecipes()
        {
            //EXTEND
            if (Extensions.ExtensionApp.extendMyApp
                (MethodBase.GetCurrentMethod().Name) != null)
            {
                return;
            }

            //CÓDIGO
            //Manual, no editor, sorry :/

            //Espada pequena
            MapStruct.craftrecipe[2, 1, 1].type = 1;
            MapStruct.craftrecipe[2, 1, 1].num = 46;
            MapStruct.craftrecipe[2, 1, 1].value = 2;
            MapStruct.craftrecipe[2, 1, 1].refin = 0;

            MapStruct.craftrecipe[2, 1, 2].type = 1;
            MapStruct.craftrecipe[2, 1, 2].num = 47;
            MapStruct.craftrecipe[2, 1, 2].value = 1;
            MapStruct.craftrecipe[2, 1, 2].refin = 0;

            //Espada Comum
            MapStruct.craftrecipe[2, 2, 1].type = 1;
            MapStruct.craftrecipe[2, 2, 1].num = 46;
            MapStruct.craftrecipe[2, 2, 1].value = 5;
            MapStruct.craftrecipe[2, 2, 1].refin = 0;

            MapStruct.craftrecipe[2, 2, 2].type = 1;
            MapStruct.craftrecipe[2, 2, 2].num = 47;
            MapStruct.craftrecipe[2, 2, 2].value = 3;
            MapStruct.craftrecipe[2, 2, 2].refin = 0;

            //Espada de Invartam
            MapStruct.craftrecipe[2, 3, 1].type = 1;
            MapStruct.craftrecipe[2, 3, 1].num = 46;
            MapStruct.craftrecipe[2, 3, 1].value = 6;
            MapStruct.craftrecipe[2, 3, 1].refin = 0;

            MapStruct.craftrecipe[2, 3, 2].type = 1;
            MapStruct.craftrecipe[2, 3, 2].num = 47;
            MapStruct.craftrecipe[2, 3, 2].value = 4;
            MapStruct.craftrecipe[2, 3, 2].refin = 0;

            MapStruct.craftrecipe[2, 3, 3].type = 1;
            MapStruct.craftrecipe[2, 3, 3].num = 12;
            MapStruct.craftrecipe[2, 3, 3].value = 2;
            MapStruct.craftrecipe[2, 3, 3].refin = 0;

            //Espada do Velho Espadachim
            MapStruct.craftrecipe[2, 4, 1].type = 1;
            MapStruct.craftrecipe[2, 4, 1].num = 46;
            MapStruct.craftrecipe[2, 4, 1].value = 10;
            MapStruct.craftrecipe[2, 4, 1].refin = 0;

            MapStruct.craftrecipe[2, 4, 2].type = 1;
            MapStruct.craftrecipe[2, 4, 2].num = 47;
            MapStruct.craftrecipe[2, 4, 2].value = 8;
            MapStruct.craftrecipe[2, 4, 2].refin = 0;

            MapStruct.craftrecipe[2, 4, 3].type = 1;
            MapStruct.craftrecipe[2, 4, 3].num = 12;
            MapStruct.craftrecipe[2, 4, 3].value = 6;
            MapStruct.craftrecipe[2, 4, 3].refin = 0;

            //Florete
            MapStruct.craftrecipe[2, 5, 1].type = 1;
            MapStruct.craftrecipe[2, 5, 1].num = 46;
            MapStruct.craftrecipe[2, 5, 1].value = 15;
            MapStruct.craftrecipe[2, 5, 1].refin = 0;

            MapStruct.craftrecipe[2, 5, 2].type = 1;
            MapStruct.craftrecipe[2, 5, 2].num = 47;
            MapStruct.craftrecipe[2, 5, 2].value = 9;
            MapStruct.craftrecipe[2, 5, 2].refin = 0;

            MapStruct.craftrecipe[2, 5, 3].type = 1;
            MapStruct.craftrecipe[2, 5, 3].num = 14;
            MapStruct.craftrecipe[2, 5, 3].value = 9;
            MapStruct.craftrecipe[2, 5, 3].refin = 0;

            //Florete do Capitão
            MapStruct.craftrecipe[2, 6, 1].type = 1;
            MapStruct.craftrecipe[2, 6, 1].num = 46;
            MapStruct.craftrecipe[2, 6, 1].value = 25;
            MapStruct.craftrecipe[2, 6, 1].refin = 0;

            MapStruct.craftrecipe[2, 6, 2].type = 1;
            MapStruct.craftrecipe[2, 6, 2].num = 47;
            MapStruct.craftrecipe[2, 6, 2].value = 9;
            MapStruct.craftrecipe[2, 6, 2].refin = 0;

            MapStruct.craftrecipe[2, 6, 3].type = 1;
            MapStruct.craftrecipe[2, 6, 3].num = 14;
            MapStruct.craftrecipe[2, 6, 3].value = 9;
            MapStruct.craftrecipe[2, 6, 3].refin = 0;

            MapStruct.craftrecipe[2, 6, 4].type = 1;
            MapStruct.craftrecipe[2, 6, 4].num = 9;
            MapStruct.craftrecipe[2, 6, 4].value = 1;
            MapStruct.craftrecipe[2, 6, 4].refin = 0;

            //Espada Longa
            MapStruct.craftrecipe[2, 7, 1].type = 1;
            MapStruct.craftrecipe[2, 7, 1].num = 46;
            MapStruct.craftrecipe[2, 7, 1].value = 30;
            MapStruct.craftrecipe[2, 7, 1].refin = 0;

            MapStruct.craftrecipe[2, 7, 2].type = 1;
            MapStruct.craftrecipe[2, 7, 2].num = 47;
            MapStruct.craftrecipe[2, 7, 2].value = 15;
            MapStruct.craftrecipe[2, 7, 2].refin = 0;

            MapStruct.craftrecipe[2, 7, 3].type = 1;
            MapStruct.craftrecipe[2, 7, 3].num = 10;
            MapStruct.craftrecipe[2, 7, 3].value = 12;
            MapStruct.craftrecipe[2, 7, 3].refin = 0;

            //Espada Longa do Andarilho
            MapStruct.craftrecipe[2, 8, 1].type = 1;
            MapStruct.craftrecipe[2, 8, 1].num = 46;
            MapStruct.craftrecipe[2, 8, 1].value = 35;
            MapStruct.craftrecipe[2, 8, 1].refin = 0;

            MapStruct.craftrecipe[2, 8, 2].type = 1;
            MapStruct.craftrecipe[2, 8, 2].num = 47;
            MapStruct.craftrecipe[2, 8, 2].value = 20;
            MapStruct.craftrecipe[2, 8, 2].refin = 0;

            MapStruct.craftrecipe[2, 8, 3].type = 1;
            MapStruct.craftrecipe[2, 8, 3].num = 10;
            MapStruct.craftrecipe[2, 8, 3].value = 12;
            MapStruct.craftrecipe[2, 8, 3].refin = 0;

            MapStruct.craftrecipe[2, 8, 4].type = 1;
            MapStruct.craftrecipe[2, 8, 4].num = 8;
            MapStruct.craftrecipe[2, 8, 4].value = 10;
            MapStruct.craftrecipe[2, 8, 4].refin = 0;

            //Espada Pesada
            MapStruct.craftrecipe[2, 9, 1].type = 1;
            MapStruct.craftrecipe[2, 9, 1].num = 46;
            MapStruct.craftrecipe[2, 9, 1].value = 45;
            MapStruct.craftrecipe[2, 9, 1].refin = 0;

            MapStruct.craftrecipe[2, 9, 2].type = 1;
            MapStruct.craftrecipe[2, 9, 2].num = 47;
            MapStruct.craftrecipe[2, 9, 2].value = 40;
            MapStruct.craftrecipe[2, 9, 2].refin = 0;

            MapStruct.craftrecipe[2, 9, 3].type = 1;
            MapStruct.craftrecipe[2, 9, 3].num = 18;
            MapStruct.craftrecipe[2, 9, 3].value = 5;
            MapStruct.craftrecipe[2, 9, 3].refin = 0;

            //Espada Pesada do Guardião
            MapStruct.craftrecipe[2, 10, 1].type = 1;
            MapStruct.craftrecipe[2, 10, 1].num = 46;
            MapStruct.craftrecipe[2, 10, 1].value = 55;
            MapStruct.craftrecipe[2, 10, 1].refin = 0;

            MapStruct.craftrecipe[2, 10, 2].type = 1;
            MapStruct.craftrecipe[2, 10, 2].num = 47;
            MapStruct.craftrecipe[2, 10, 2].value = 50;
            MapStruct.craftrecipe[2, 10, 2].refin = 0;

            MapStruct.craftrecipe[2, 10, 3].type = 1;
            MapStruct.craftrecipe[2, 10, 3].num = 18;
            MapStruct.craftrecipe[2, 10, 3].value = 5;
            MapStruct.craftrecipe[2, 10, 3].refin = 0;

            MapStruct.craftrecipe[2, 10, 4].type = 1;
            MapStruct.craftrecipe[2, 10, 4].num = 19;
            MapStruct.craftrecipe[2, 10, 4].value = 5;
            MapStruct.craftrecipe[2, 10, 4].refin = 0;

            //Espada do Antigo Rei
            MapStruct.craftrecipe[2, 11, 1].type = 1;
            MapStruct.craftrecipe[2, 11, 1].num = 46;
            MapStruct.craftrecipe[2, 11, 1].value = 55;
            MapStruct.craftrecipe[2, 11, 1].refin = 0;

            MapStruct.craftrecipe[2, 11, 2].type = 1;
            MapStruct.craftrecipe[2, 11, 2].num = 47;
            MapStruct.craftrecipe[2, 11, 2].value = 50;
            MapStruct.craftrecipe[2, 11, 2].refin = 0;

            MapStruct.craftrecipe[2, 11, 3].type = 1;
            MapStruct.craftrecipe[2, 11, 3].num = 18;
            MapStruct.craftrecipe[2, 11, 3].value = 5;
            MapStruct.craftrecipe[2, 11, 3].refin = 0;

            MapStruct.craftrecipe[2, 11, 4].type = 1;
            MapStruct.craftrecipe[2, 11, 4].num = 19;
            MapStruct.craftrecipe[2, 11, 4].value = 5;
            MapStruct.craftrecipe[2, 11, 4].refin = 0;

            MapStruct.craftrecipe[2, 11, 5].type = 1;
            MapStruct.craftrecipe[2, 11, 5].num = 34;
            MapStruct.craftrecipe[2, 11, 5].value = 50;
            MapStruct.craftrecipe[2, 11, 5].refin = 0;

            //Aiguillon
            MapStruct.craftrecipe[2, 12, 1].type = 1;
            MapStruct.craftrecipe[2, 12, 1].num = 46;
            MapStruct.craftrecipe[2, 12, 1].value = 100;
            MapStruct.craftrecipe[2, 12, 1].refin = 0;

            MapStruct.craftrecipe[2, 12, 2].type = 1;
            MapStruct.craftrecipe[2, 12, 2].num = 47;
            MapStruct.craftrecipe[2, 12, 2].value = 75;
            MapStruct.craftrecipe[2, 12, 2].refin = 0;

            MapStruct.craftrecipe[2, 12, 3].type = 1;
            MapStruct.craftrecipe[2, 12, 3].num = 38;
            MapStruct.craftrecipe[2, 12, 3].value = 100;
            MapStruct.craftrecipe[2, 12, 3].refin = 0;

            MapStruct.craftrecipe[2, 12, 4].type = 1;
            MapStruct.craftrecipe[2, 12, 4].num = 39;
            MapStruct.craftrecipe[2, 12, 4].value = 5;
            MapStruct.craftrecipe[2, 12, 4].refin = 0;

            MapStruct.craftrecipe[2, 12, 5].type = 1;
            MapStruct.craftrecipe[2, 12, 5].num = 40;
            MapStruct.craftrecipe[2, 12, 5].value = 1;
            MapStruct.craftrecipe[2, 12, 5].refin = 0;

            //Aiguille
            MapStruct.craftrecipe[2, 13, 1].type = 1;
            MapStruct.craftrecipe[2, 13, 1].num = 46;
            MapStruct.craftrecipe[2, 13, 1].value = 100;
            MapStruct.craftrecipe[2, 13, 1].refin = 0;

            MapStruct.craftrecipe[2, 13, 2].type = 1;
            MapStruct.craftrecipe[2, 13, 2].num = 47;
            MapStruct.craftrecipe[2, 13, 2].value = 86;
            MapStruct.craftrecipe[2, 13, 2].refin = 0;

            MapStruct.craftrecipe[2, 13, 3].type = 1;
            MapStruct.craftrecipe[2, 13, 3].num = 41;
            MapStruct.craftrecipe[2, 13, 3].value = 100;
            MapStruct.craftrecipe[2, 13, 3].refin = 0;

            MapStruct.craftrecipe[2, 13, 4].type = 1;
            MapStruct.craftrecipe[2, 13, 4].num = 39;
            MapStruct.craftrecipe[2, 13, 4].value = 5;
            MapStruct.craftrecipe[2, 13, 4].refin = 0;

            MapStruct.craftrecipe[2, 13, 5].type = 1;
            MapStruct.craftrecipe[2, 13, 5].num = 40;
            MapStruct.craftrecipe[2, 13, 5].value = 1;
            MapStruct.craftrecipe[2, 13, 5].refin = 0;

            //Radiata
            MapStruct.craftrecipe[2, 14, 1].type = 1;
            MapStruct.craftrecipe[2, 14, 1].num = 46;
            MapStruct.craftrecipe[2, 14, 1].value = 100;
            MapStruct.craftrecipe[2, 14, 1].refin = 0;

            MapStruct.craftrecipe[2, 14, 2].type = 1;
            MapStruct.craftrecipe[2, 14, 2].num = 47;
            MapStruct.craftrecipe[2, 14, 2].value = 86;
            MapStruct.craftrecipe[2, 14, 2].refin = 0;

            MapStruct.craftrecipe[2, 14, 3].type = 1;
            MapStruct.craftrecipe[2, 14, 3].num = 43;
            MapStruct.craftrecipe[2, 14, 3].value = 100;
            MapStruct.craftrecipe[2, 14, 3].refin = 0;

            MapStruct.craftrecipe[2, 14, 4].type = 1;
            MapStruct.craftrecipe[2, 14, 4].num = 37;
            MapStruct.craftrecipe[2, 14, 4].value = 5;
            MapStruct.craftrecipe[2, 14, 4].refin = 0;

            MapStruct.craftrecipe[2, 14, 5].type = 1;
            MapStruct.craftrecipe[2, 14, 5].num = 42;
            MapStruct.craftrecipe[2, 14, 5].value = 1;
            MapStruct.craftrecipe[2, 14, 5].refin = 0;

            //Fantome
            MapStruct.craftrecipe[2, 15, 1].type = 1;
            MapStruct.craftrecipe[2, 15, 1].num = 46;
            MapStruct.craftrecipe[2, 15, 1].value = 100;
            MapStruct.craftrecipe[2, 15, 1].refin = 0;

            MapStruct.craftrecipe[2, 15, 2].type = 1;
            MapStruct.craftrecipe[2, 15, 2].num = 47;
            MapStruct.craftrecipe[2, 15, 2].value = 86;
            MapStruct.craftrecipe[2, 15, 2].refin = 0;

            MapStruct.craftrecipe[2, 15, 3].type = 1;
            MapStruct.craftrecipe[2, 15, 3].num = 36;
            MapStruct.craftrecipe[2, 15, 3].value = 100;
            MapStruct.craftrecipe[2, 15, 3].refin = 0;

            MapStruct.craftrecipe[2, 15, 4].type = 1;
            MapStruct.craftrecipe[2, 15, 4].num = 40;
            MapStruct.craftrecipe[2, 15, 4].value = 10;
            MapStruct.craftrecipe[2, 15, 4].refin = 0;

            MapStruct.craftrecipe[2, 15, 5].type = 1;
            MapStruct.craftrecipe[2, 15, 5].num = 35;
            MapStruct.craftrecipe[2, 15, 5].value = 10;
            MapStruct.craftrecipe[2, 15, 5].refin = 0;

            //Volonte
            MapStruct.craftrecipe[2, 16, 1].type = 1;
            MapStruct.craftrecipe[2, 16, 1].num = 46;
            MapStruct.craftrecipe[2, 16, 1].value = 100;
            MapStruct.craftrecipe[2, 16, 1].refin = 0;

            MapStruct.craftrecipe[2, 16, 2].type = 1;
            MapStruct.craftrecipe[2, 16, 2].num = 47;
            MapStruct.craftrecipe[2, 16, 2].value = 86;
            MapStruct.craftrecipe[2, 16, 2].refin = 0;

            MapStruct.craftrecipe[2, 16, 3].type = 1;
            MapStruct.craftrecipe[2, 16, 3].num = 26;
            MapStruct.craftrecipe[2, 16, 3].value = 100;
            MapStruct.craftrecipe[2, 16, 3].refin = 0;

            MapStruct.craftrecipe[2, 16, 4].type = 1;
            MapStruct.craftrecipe[2, 16, 4].num = 39;
            MapStruct.craftrecipe[2, 16, 4].value = 1;
            MapStruct.craftrecipe[2, 16, 4].refin = 0;

            MapStruct.craftrecipe[2, 16, 5].type = 1;
            MapStruct.craftrecipe[2, 16, 5].num = 40;
            MapStruct.craftrecipe[2, 16, 5].value = 10;
            MapStruct.craftrecipe[2, 16, 5].refin = 0;

            //Rocher
            MapStruct.craftrecipe[2, 17, 1].type = 1;
            MapStruct.craftrecipe[2, 17, 1].num = 46;
            MapStruct.craftrecipe[2, 17, 1].value = 100;
            MapStruct.craftrecipe[2, 17, 1].refin = 0;

            MapStruct.craftrecipe[2, 17, 2].type = 1;
            MapStruct.craftrecipe[2, 17, 2].num = 47;
            MapStruct.craftrecipe[2, 17, 2].value = 90;
            MapStruct.craftrecipe[2, 17, 2].refin = 0;

            MapStruct.craftrecipe[2, 17, 3].type = 1;
            MapStruct.craftrecipe[2, 17, 3].num = 32;
            MapStruct.craftrecipe[2, 17, 3].value = 100;
            MapStruct.craftrecipe[2, 17, 3].refin = 0;

            MapStruct.craftrecipe[2, 17, 4].type = 1;
            MapStruct.craftrecipe[2, 17, 4].num = 22;
            MapStruct.craftrecipe[2, 17, 4].value = 1;
            MapStruct.craftrecipe[2, 17, 4].refin = 0;

            MapStruct.craftrecipe[2, 17, 5].type = 1;
            MapStruct.craftrecipe[2, 17, 5].num = 40;
            MapStruct.craftrecipe[2, 17, 5].value = 10;
            MapStruct.craftrecipe[2, 17, 5].refin = 0;

            //Brule
            MapStruct.craftrecipe[2, 18, 1].type = 1;
            MapStruct.craftrecipe[2, 18, 1].num = 46;
            MapStruct.craftrecipe[2, 18, 1].value = 100;
            MapStruct.craftrecipe[2, 18, 1].refin = 0;

            MapStruct.craftrecipe[2, 18, 2].type = 1;
            MapStruct.craftrecipe[2, 18, 2].num = 47;
            MapStruct.craftrecipe[2, 18, 2].value = 90;
            MapStruct.craftrecipe[2, 18, 2].refin = 0;

            MapStruct.craftrecipe[2, 18, 3].type = 1;
            MapStruct.craftrecipe[2, 18, 3].num = 42;
            MapStruct.craftrecipe[2, 18, 3].value = 56;
            MapStruct.craftrecipe[2, 18, 3].refin = 0;

            MapStruct.craftrecipe[2, 18, 4].type = 1;
            MapStruct.craftrecipe[2, 18, 4].num = 17;
            MapStruct.craftrecipe[2, 18, 4].value = 100;
            MapStruct.craftrecipe[2, 18, 4].refin = 0;

            MapStruct.craftrecipe[2, 18, 5].type = 1;
            MapStruct.craftrecipe[2, 18, 5].num = 40;
            MapStruct.craftrecipe[2, 18, 5].value = 10;
            MapStruct.craftrecipe[2, 18, 5].refin = 0;

            //Grand Cri
            MapStruct.craftrecipe[2, 19, 1].type = 1;
            MapStruct.craftrecipe[2, 19, 1].num = 46;
            MapStruct.craftrecipe[2, 19, 1].value = 100;
            MapStruct.craftrecipe[2, 19, 1].refin = 0;

            MapStruct.craftrecipe[2, 19, 2].type = 1;
            MapStruct.craftrecipe[2, 19, 2].num = 47;
            MapStruct.craftrecipe[2, 19, 2].value = 100;
            MapStruct.craftrecipe[2, 19, 2].refin = 0;

            MapStruct.craftrecipe[2, 19, 3].type = 1;
            MapStruct.craftrecipe[2, 19, 3].num = 22;
            MapStruct.craftrecipe[2, 19, 3].value = 2;
            MapStruct.craftrecipe[2, 19, 3].refin = 0;

            MapStruct.craftrecipe[2, 19, 4].type = 1;
            MapStruct.craftrecipe[2, 19, 4].num = 45;
            MapStruct.craftrecipe[2, 19, 4].value = 2;
            MapStruct.craftrecipe[2, 19, 4].refin = 0;

            MapStruct.craftrecipe[2, 19, 5].type = 1;
            MapStruct.craftrecipe[2, 19, 5].num = 40;
            MapStruct.craftrecipe[2, 19, 5].value = 10;
            MapStruct.craftrecipe[2, 19, 5].refin = 0;

            //Tapegeur
            MapStruct.craftrecipe[2, 20, 1].type = 1;
            MapStruct.craftrecipe[2, 20, 1].num = 46;
            MapStruct.craftrecipe[2, 20, 1].value = 100;
            MapStruct.craftrecipe[2, 20, 1].refin = 0;

            MapStruct.craftrecipe[2, 20, 2].type = 1;
            MapStruct.craftrecipe[2, 20, 2].num = 47;
            MapStruct.craftrecipe[2, 20, 2].value = 100;
            MapStruct.craftrecipe[2, 20, 2].refin = 0;

            MapStruct.craftrecipe[2, 20, 3].type = 1;
            MapStruct.craftrecipe[2, 20, 3].num = 22;
            MapStruct.craftrecipe[2, 20, 3].value = 2;
            MapStruct.craftrecipe[2, 20, 3].refin = 0;

            MapStruct.craftrecipe[2, 20, 4].type = 1;
            MapStruct.craftrecipe[2, 20, 4].num = 28;
            MapStruct.craftrecipe[2, 20, 4].value = 1;
            MapStruct.craftrecipe[2, 20, 4].refin = 0;

            MapStruct.craftrecipe[2, 20, 5].type = 1;
            MapStruct.craftrecipe[2, 20, 5].num = 40;
            MapStruct.craftrecipe[2, 20, 5].value = 10;
            MapStruct.craftrecipe[2, 20, 5].refin = 0;

            //Muramasa
            MapStruct.craftrecipe[2, 21, 1].type = 1;
            MapStruct.craftrecipe[2, 21, 1].num = 46;
            MapStruct.craftrecipe[2, 21, 1].value = 100;
            MapStruct.craftrecipe[2, 21, 1].refin = 0;

            MapStruct.craftrecipe[2, 21, 2].type = 1;
            MapStruct.craftrecipe[2, 21, 2].num = 47;
            MapStruct.craftrecipe[2, 21, 2].value = 100;
            MapStruct.craftrecipe[2, 21, 2].refin = 0;

            MapStruct.craftrecipe[2, 21, 3].type = 1;
            MapStruct.craftrecipe[2, 21, 3].num = 28;
            MapStruct.craftrecipe[2, 21, 3].value = 1;
            MapStruct.craftrecipe[2, 21, 3].refin = 0;

            MapStruct.craftrecipe[2, 21, 4].type = 1;
            MapStruct.craftrecipe[2, 21, 4].num = 48;
            MapStruct.craftrecipe[2, 21, 4].value = 1;
            MapStruct.craftrecipe[2, 21, 4].refin = 0;

            MapStruct.craftrecipe[2, 21, 5].type = 1;
            MapStruct.craftrecipe[2, 21, 5].num = 40;
            MapStruct.craftrecipe[2, 21, 5].value = 10;
            MapStruct.craftrecipe[2, 21, 5].refin = 0;

            //Espada Secto
            MapStruct.craftrecipe[2, 22, 1].type = 1;
            MapStruct.craftrecipe[2, 22, 1].num = 46;
            MapStruct.craftrecipe[2, 22, 1].value = 50;
            MapStruct.craftrecipe[2, 22, 1].refin = 0;

            MapStruct.craftrecipe[2, 22, 2].type = 1;
            MapStruct.craftrecipe[2, 22, 2].num = 47;
            MapStruct.craftrecipe[2, 22, 2].value = 50;
            MapStruct.craftrecipe[2, 22, 2].refin = 0;

            MapStruct.craftrecipe[2, 22, 3].type = 1;
            MapStruct.craftrecipe[2, 22, 3].num = 26;
            MapStruct.craftrecipe[2, 22, 3].value = 100;
            MapStruct.craftrecipe[2, 22, 3].refin = 0;

            //Voospada
            MapStruct.craftrecipe[2, 23, 1].type = 1;
            MapStruct.craftrecipe[2, 23, 1].num = 46;
            MapStruct.craftrecipe[2, 23, 1].value = 100;
            MapStruct.craftrecipe[2, 23, 1].refin = 0;

            MapStruct.craftrecipe[2, 23, 2].type = 1;
            MapStruct.craftrecipe[2, 23, 2].num = 47;
            MapStruct.craftrecipe[2, 23, 2].value = 100;
            MapStruct.craftrecipe[2, 23, 2].refin = 0;

            MapStruct.craftrecipe[2, 23, 3].type = 1;
            MapStruct.craftrecipe[2, 23, 3].num = 23;
            MapStruct.craftrecipe[2, 23, 3].value = 100;
            MapStruct.craftrecipe[2, 23, 3].refin = 0;

            MapStruct.craftrecipe[2, 23, 4].type = 1;
            MapStruct.craftrecipe[2, 23, 4].num = 28;
            MapStruct.craftrecipe[2, 23, 4].value = 1;
            MapStruct.craftrecipe[2, 23, 4].refin = 0;

            MapStruct.craftrecipe[2, 23, 5].type = 1;
            MapStruct.craftrecipe[2, 23, 5].num = 25;
            MapStruct.craftrecipe[2, 23, 5].value = 40;
            MapStruct.craftrecipe[2, 23, 5].refin = 0;

            //Lança Feitiços
            MapStruct.craftrecipe[2, 24, 1].type = 1;
            MapStruct.craftrecipe[2, 24, 1].num = 47;
            MapStruct.craftrecipe[2, 24, 1].value = 100;
            MapStruct.craftrecipe[2, 24, 1].refin = 0;

            MapStruct.craftrecipe[2, 24, 2].type = 1;
            MapStruct.craftrecipe[2, 24, 2].num = 38;
            MapStruct.craftrecipe[2, 24, 2].value = 100;
            MapStruct.craftrecipe[2, 24, 2].refin = 0;

            MapStruct.craftrecipe[2, 24, 3].type = 1;
            MapStruct.craftrecipe[2, 24, 3].num = 34;
            MapStruct.craftrecipe[2, 24, 3].value = 30;
            MapStruct.craftrecipe[2, 24, 3].refin = 0;

            MapStruct.craftrecipe[2, 24, 4].type = 1;
            MapStruct.craftrecipe[2, 24, 4].num = 40;
            MapStruct.craftrecipe[2, 24, 4].value = 1;
            MapStruct.craftrecipe[2, 24, 4].refin = 0;

            MapStruct.craftrecipe[2, 24, 5].type = 1;
            MapStruct.craftrecipe[2, 24, 5].num = 41;
            MapStruct.craftrecipe[2, 24, 5].value = 2;
            MapStruct.craftrecipe[2, 24, 5].refin = 0;

            //Cajado do Antigo Carvalho
            MapStruct.craftrecipe[2, 25, 1].type = 1;
            MapStruct.craftrecipe[2, 25, 1].num = 47;
            MapStruct.craftrecipe[2, 25, 1].value = 100;
            MapStruct.craftrecipe[2, 25, 1].refin = 0;

            MapStruct.craftrecipe[2, 25, 2].type = 1;
            MapStruct.craftrecipe[2, 25, 2].num = 43;
            MapStruct.craftrecipe[2, 25, 2].value = 200;
            MapStruct.craftrecipe[2, 25, 2].refin = 0;

            MapStruct.craftrecipe[2, 25, 3].type = 1;
            MapStruct.craftrecipe[2, 25, 3].num = 40;
            MapStruct.craftrecipe[2, 25, 3].value = 1;
            MapStruct.craftrecipe[2, 25, 3].refin = 0;

            //Fim Mif
            MapStruct.craftrecipe[2, 26, 1].type = 1;
            MapStruct.craftrecipe[2, 26, 1].num = 46;
            MapStruct.craftrecipe[2, 26, 1].value = 100;
            MapStruct.craftrecipe[2, 26, 1].refin = 0;

            MapStruct.craftrecipe[2, 26, 2].type = 1;
            MapStruct.craftrecipe[2, 26, 2].num = 47;
            MapStruct.craftrecipe[2, 26, 2].value = 100;
            MapStruct.craftrecipe[2, 26, 2].refin = 0;

            MapStruct.craftrecipe[2, 26, 3].type = 1;
            MapStruct.craftrecipe[2, 26, 3].num = 37;
            MapStruct.craftrecipe[2, 26, 3].value = 200;
            MapStruct.craftrecipe[2, 26, 3].refin = 0;

            //Oacidlam
            MapStruct.craftrecipe[2, 27, 1].type = 1;
            MapStruct.craftrecipe[2, 27, 1].num = 46;
            MapStruct.craftrecipe[2, 27, 1].value = 100;
            MapStruct.craftrecipe[2, 27, 1].refin = 0;

            MapStruct.craftrecipe[2, 27, 2].type = 1;
            MapStruct.craftrecipe[2, 27, 2].num = 47;
            MapStruct.craftrecipe[2, 27, 2].value = 100;
            MapStruct.craftrecipe[2, 27, 2].refin = 0;

            MapStruct.craftrecipe[2, 27, 3].type = 1;
            MapStruct.craftrecipe[2, 27, 3].num = 34;
            MapStruct.craftrecipe[2, 27, 3].value = 100;
            MapStruct.craftrecipe[2, 27, 3].refin = 0;

            MapStruct.craftrecipe[2, 27, 4].type = 1;
            MapStruct.craftrecipe[2, 27, 4].num = 28;
            MapStruct.craftrecipe[2, 27, 4].value = 1;
            MapStruct.craftrecipe[2, 27, 4].refin = 0;

            MapStruct.craftrecipe[2, 27, 5].type = 1;
            MapStruct.craftrecipe[2, 27, 5].num = 40;
            MapStruct.craftrecipe[2, 27, 5].value = 130;
            MapStruct.craftrecipe[2, 27, 5].refin = 0;

            //Picareta
            MapStruct.craftrecipe[2, 28, 1].type = 1;
            MapStruct.craftrecipe[2, 28, 1].num = 46;
            MapStruct.craftrecipe[2, 28, 1].value = 2;
            MapStruct.craftrecipe[2, 28, 1].refin = 0;

            MapStruct.craftrecipe[2, 28, 2].type = 1;
            MapStruct.craftrecipe[2, 28, 2].num = 47;
            MapStruct.craftrecipe[2, 28, 2].value = 1;
            MapStruct.craftrecipe[2, 28, 2].refin = 0;

            //Clava Kiorkle
            MapStruct.craftrecipe[2, 29, 1].type = 1;
            MapStruct.craftrecipe[2, 29, 1].num = 46;
            MapStruct.craftrecipe[2, 29, 1].value = 50;
            MapStruct.craftrecipe[2, 29, 1].refin = 0;

            MapStruct.craftrecipe[2, 29, 2].type = 1;
            MapStruct.craftrecipe[2, 29, 2].num = 47;
            MapStruct.craftrecipe[2, 29, 2].value = 50;
            MapStruct.craftrecipe[2, 29, 2].refin = 0;

            //Machado do Golem
            MapStruct.craftrecipe[2, 30, 1].type = 1;
            MapStruct.craftrecipe[2, 30, 1].num = 46;
            MapStruct.craftrecipe[2, 30, 1].value = 1000;
            MapStruct.craftrecipe[2, 30, 1].refin = 0;

            MapStruct.craftrecipe[2, 30, 2].type = 1;
            MapStruct.craftrecipe[2, 30, 2].num = 47;
            MapStruct.craftrecipe[2, 30, 2].value = 100;
            MapStruct.craftrecipe[2, 30, 2].refin = 0;

            //Martelo
            MapStruct.craftrecipe[2, 31, 1].type = 1;
            MapStruct.craftrecipe[2, 31, 1].num = 46;
            MapStruct.craftrecipe[2, 31, 1].value = 2;
            MapStruct.craftrecipe[2, 31, 1].refin = 0;

            MapStruct.craftrecipe[2, 31, 2].type = 1;
            MapStruct.craftrecipe[2, 31, 2].num = 47;
            MapStruct.craftrecipe[2, 31, 2].value = 1;
            MapStruct.craftrecipe[2, 31, 2].refin = 0;
        }
    }
}
