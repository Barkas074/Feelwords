﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Feelwords
{
	class TheNamesOfTheItems
	{
		protected readonly static string[] NAMEGAME = { "    #     ##   ##    #####  ######    #####   ######     ####   ##   ## ",
														"  #####   ##  ###   ##  ##  ##   ##  ##   ##  ##   ##   ## ##   ##   ## ",
														" ## # ##  ##  ###   ##  ##  ##   ##  ##   ##  ##   ##   ## ##   ##   ## ",
														" ## # ##  ## # ##   ##  ##  ######   ##   ##  ######    ## ##   ####  # ",
														" ## # ##  ###  ##   ##  ##  ##   ##  ##   ##  ##        ## ##   ##  # # ",
														"  #####   ##   ##   ##  ##  ##   ##  ##   ##  ##       #######  ##  # # ",
														"    #     ##   ##  ##   ##  ######    #####   ##       ##   ##  ####  # ",
														"                                                                        " };
		protected readonly static string[] NEWGAME = { " ##   ##                                                                             ",
													   " ##   ##                                                                             ",
													   " ##   ##   #####   #####     ####      #####      ##   ##   #####   ######    ####   ",
													   " #######  ##   ##  ##  ##       ##    ##  ##      ##  ###       ##  ##   ##      ##  ",
													   " ##   ##  ##   ##  ######    #####     #####      ## # ##   #####   ######    #####  ",
													   " ##   ##  ##   ##  ##   ##  ##  ##    ##  ##      ###  ##  ##       ##       ##  ##  ",
													   " ##   ##   #####   ######    ######  ##   ##      ##   ##   ######  ##        ###### ",
													   "                                                                                     " };
		protected readonly static string[] RESUME =  { " #######                                                                                  ",
													   " ##   ##                      ####                                                        ",
													   " ##   ##  ######    #####        ##   #####     #####  ## # ##  ##   ##  ######   ##      ",
													   " ##   ##  ##   ##  ##   ##    #####  ##   ##   ##  ##   # # #   ##  ###    ##     ##      ",
													   " ##   ##  ######   ##   ##   ##  ##  ##   ##   ##  ##    ###    ## # ##    ##     ######  ",
													   " ##   ##  ##       ##   ##  ##   ##  ##   ##   ##  ##   # # #   ###  ##    ##     ##   ## ",
													   " ##   ##  ##        #####    #####    #####   ##   ##  ## # ##  ##   ##    ##     ######  ",
													   "                                                                                          " };
		protected readonly static string[] RATING =  { " ######                                                        ",
													   " ##   ##              ##                                       ",
													   " ##   ##   #####   ##   ##  ######   ##   ##  ##   ##   #####  ",
													   " ######   ##   ##  ##  ###    ##     ##  ###  ##   ##       ## ",
													   " ##       ######   ## # ##    ##     ## # ##  #######   #####  ",
													   " ##       ##       ###  ##    ##     ###  ##  ##   ##  ##      ",
													   " ##        #####   ##   ##    ##     ##   ##  ##   ##   ###### ",
													   "                                                               " };
		protected readonly static string[] EXIT = { " ######                                      ",
													" ##   ##                               ####  ",
													" ##   ##  ##   ##  ##   ##   #####        ## ",
													" ######   ##   ##   ## ##   ##   ##    ##### ",
													" ##   ##  ####  #    ###    ##   ##   ##  ## ",
													" ##   ##  ##  # #   ## ##   ##   ##  ##   ## ",
													" ######   ####  #  ##   ##   #####    #####  ",
													"                                             " };
		protected readonly static string[] DUMMY = { " ######                                                                                                                                        ",
													 "   ##                                      ####                                ####                    ##              ####                    ",
													 "   ##     ##   ##  ######        #####        ##  ##   ##   ####    ## # ##       ##  ##   ##       ####    ##   ##       ##   #####   ######  ",
													 "   ##     ##   ##    ##         ##   ##    #####  ##   ##      ##    # # #     #####  ##   ##      ##       ##   ##    #####  ##   ##    ##    ",
													 "   ##     ##   ##    ##         ##   ##   ##  ##  #######   #####     ###     ##  ##  ####  #      ######   ##   ##   ##  ##  ######     ##    ",
													 "   ##      ######    ##         ##   ##  ##   ##  ##   ##  ##  ##    # # #   ##   ##  ##  # #      ##   ##   ######  ##   ##  ##         ##    ",
													 "   ##          ##    ##          #####    #####   ##   ##   ######  ## # ##   #####   ####  #       #####        ##   #####    #####     ##    ",
													 "           #####                                                                                             #####                             ",
													 "                                                                                                                                               " };
		protected readonly static string[] NAMEREQUEST = { " ##   ##                        ######                                                                       #####  ",
														   " ##  ##                         ##   ##                                                                     #     # ",
														   " ## ##     ####    ##   ##      ##   ##   ####     #####        #####    #####   #####    ##   ##  ######         # ",
														   " #####        ##   ##  ##       ######       ##   ##           ##   ##  ##   ##  ##  ##   ##   ##    ##        ###  ",
														   " ##  ##    #####   #####        ##   ##   #####   ##              ###   ##   ##  ######   ##   ##    ##        #    ",
														   " ##   ##  ##  ##   ##  ##       ##   ##  ##  ##   ##           ##   ##  ##   ##  ##   ##   ######    ##             ",
														   " ##   ##   ######  ##   ##      ######    ######   #####        #####    #####   ######        ##    ##        #    ",
														   "                                                                                           #####                    " };
		protected readonly string[][] menuOption = { NAMEGAME, NEWGAME, RESUME, RATING, EXIT, DUMMY };
	}
}
