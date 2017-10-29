grammar Metar;

@header {
#pragma warning disable 3021
}

/*
 * Parser Rules
 */

// *_opt means that this rule is optional (element may not be present in actual text)
message : header body_opt END EOF;
header : CODE WS INDEX WS DATETIME;//cor_opt nil_opt auto_opt;
body_opt : body | ;
body : WS wind;
wind : WIND_DIRECTION WIND_SPEED wind_gust_opt WIND_SPEED_UNIT wind_change_opt;
//cor_opt : CORRECTION | ;
//nil_opt : NIL | ;
//auto_opt : AUTO | ;
wind_gust_opt : WIND_GUST | ;
wind_change_opt : wind_change | ;
wind_change : WS WIND_CHANGE;

/*
 * Lexers Rules
 */

CODE : 'METAR' | 'SPECI';
INDEX : FOUR_CHARS;
DATETIME : SIX_DIGITS 'Z';
//CORRECTION : 'COR';
//NIL : 'NIL';
//AUTO : 'AUTO';
WIND_DIRECTION : THREE_DIGITS | 'VRB';
WIND_SPEED : 'P' TWO_DIGITS | TWO_DIGITS;
WIND_GUST : 'G' TWO_DIGITS;
WIND_SPEED_UNIT : 'MPS' | 'KT';
WIND_CHANGE : THREE_DIGITS 'V' THREE_DIGITS;

fragment FOUR_CHARS : CH CH CH CH;
fragment SIX_DIGITS : D D D D D D;
fragment THREE_DIGITS : D D D;
fragment TWO_DIGITS : D D;
fragment CH : [A-Z];
fragment D : [0-9];

END : WS '=' | '=';
WS : (' '|'\t')+;
NEWLINE : ('\r'? '\n' | '\r')+ -> skip;