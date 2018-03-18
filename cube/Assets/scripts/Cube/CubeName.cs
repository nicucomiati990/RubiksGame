using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CubeName {
	// centers
	White, Yellow, Red, Orange, Green, Blue,
	// white edges
	WhiteRed, WhiteOrange, WhiteBlue, WhiteGreen,
	// yellow edges
	YellowRed, YellowOrange, YellowBlue, YellowGreen,
	// other edges
	RedBlue, RedGreen, OrangeBlue, OrangeGreen,
	// white corners
	WhiteRedBlue, WhiteOrangeGreen, WhiteOrangeBlue, WhiteRedGreen,
	// yellow corners
	YellowRedBlue, YellowOrangeGreen, YellowOrangeBlue, YellowRedGreen
}
