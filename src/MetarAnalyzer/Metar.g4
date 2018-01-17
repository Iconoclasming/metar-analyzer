grammar Metar;

@header {
#pragma warning disable 3021
}

/*
 * Parser Rules
 */

// *_opt means that this rule is optional (element may not be present in actual text)
message : header body_opt END EOF;
header : CODE cor_opt WORD_ONLY_WITH_CHARS DATETIME nil_opt; // auto_opt;
body_opt : body | ;
body : wind visibility_opt current_weather_opt;
wind : WIND_WITH_CHANGE ;
cor_opt : CORRECTION | ;
nil_opt : NIL | ;
//auto_opt : AUTO | ;
wind_change_opt : WIND_CHANGE | ;
//cavok_opt : WS CAVOK | ;
visibility_opt : visibility | ;
visibility : VISIBILITY visibility_direction_opt;
visibility_direction_opt : VISIBILITY_DIRECTION | ;
current_weather_opt : current_weather | ;
current_weather : WORD_ONLY_WITH_CHARS | WORD_ONLY_WITH_CHARS WS WORD_ONLY_WITH_CHARS;

/*
 * Lexers Rules
 */

CODE : 'METAR' | 'SPECI';
DATETIME : D D D D D D 'Z';
CORRECTION : 'COR';
NIL : 'NIL';
//AUTO : 'AUTO';
WIND_WITH_CHANGE : WIND | WIND WS WIND_CHANGE;
WIND : WIND_DIRECTION WIND_SPEED WIND_GUST WIND_SPEED_UNIT | WIND_DIRECTION WIND_SPEED WIND_SPEED_UNIT;
WIND_DIRECTION : D D D | 'VRB';
WIND_SPEED : 'P' D D | D D;
WIND_GUST : 'G' D D;
WIND_SPEED_UNIT : 'MPS' | 'KT';
WIND_CHANGE : D D D 'V' D D D;
VISIBILITY : D D D D;
VISIBILITY_DIRECTION : D D D D CH CH;
WORD_ONLY_WITH_CHARS : CH+;
//CAVOK : 'CAVOK';

END : WS '=' | '=';
WS : (' '|'\t')+ -> channel(HIDDEN);
NEWLINE : ('\r'? '\n' | '\r')+ -> skip;

fragment CH : [A-Z];
fragment D : [0-9];
fragment CHAR_DIGIT : [A-Z0-9];
