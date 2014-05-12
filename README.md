###Опис

Играта Space Wars е резултат на инспирација од многуте tower defence игри кои се достапни на пазарот. Нашата цел беше играта да биде направена со чист и едноставен дизајн со цел да биде привлечна и интересна за корисникот. Играта нуди прекрасен интерактивен дизајн притоа имплементирајќи алгоритми за движење, детекција на колизија, пресметка на растојание, следење на цел, исцртување функции итн. Главната цел на Space Wars е да му понуди на корисникот начин да се релаксира и да избега од секојдневието со отварање на портите на вселенскиот мистичен амбиент. 
 

Со самото стартување на играта се отвора главното мени кое е доста едноставно и нуди три опции:

1) Start - за почеток на нова игра.

2) Highscores - за преглед на најдобрите одиграни резултати.

3) Exit - за излез од играта.

 
Со притискање на копчето Start играта веднаш започнува. Се појавува мапата на која е очигледна патеката по која ќе се движат непријателите. На десната страна имаме статусна лента која најгоре ги содржи сите информации за моменталната игра која се од значење за корисниот (животи, пари, резултат и време). 
Веднаш под нив се наоѓа менито за избор на кула за одбрана. Во играта се имплементирани пет различни видови на кули. Секоја од нив има различна цена и се разбира различни спецификации. Спецификациите на кулата се однесуваат на брзина на пукање, колку штета причинуваат нивние куршуми и домет на куршумите.
Во почетокот играчот може да изгради само една истанца од првата кула. Со уништување на непријателските бродови играчот добива пари со кои може да гради и поскапи кули. Резултатот се пресметува со бројот и типот на уништени непријатели.

 
Непријателите доаѓаат во бранови на извесен временски интервал. Секој бран си носи своја тежина. Се разбира со текот на времето тежината на брановите (бројот и типот на непријатели) се зголемува. Секој тип на непријател се одликува со брзина и живот. Со намалување на животот на непријателот (со пукање од страна на кулите) се намалува и неговата големина.
Животот на играчот се намалува доколку тој не успее да уништи некој непријател и тој стигне до крајот на патеката. Доколку животот на играчот дојде до 0 играта завршува и на играчот му се нудат истите три опции како и во почетното мени.
 

Со клик на некоја од кулите од десната статусна лента на покажувачот на маусот се исцртува кулата (во транспарентна форма) која сакаме да ја изградиме и соодветно нејзинот домет на стрелање.
 
Со клик некаде на мапата кулата се гради и таа е веднаш функционална. Доколку кликнеме врз патеката или врз друга кула градењето нема да биде дозволено и ќе мораме да избереме друга локација за кулата. Доколку се предомислиме и не сакаме да ја изградиме кулата едноставно притискаме ESC.
Играта има и опција за пауза која се активира со едноставно притискање на копчето P. Кога сакаме да се вратиме назад во играта повторно го притискаме истото копче.
 
Доколку успееме да ги уништиме сите непријателски бранови го победување нивото на играта и ни се нуди избор за повторно играње, најдобри резултати или излез.
 
Доколку кликнеме на копчето highscores ќе ни се отвори нова форма која ќе ги содржи резултатите од сите предходни играња кои ќе бидат сортирани во опаѓачки редослед. 
Резултатите во играта се зачувуваат локално на компјутерот со што со било кое стартување на играта резултатите од изминатите играња се зачувани и прочитани од локална датотека.
 
###Технички дел 

Кодот на овој проект е структуриран во класна хиерархија. На врвот на таа хиерархија ја имаме класата Entity во која чуваме само локацијата и изгледот на објектот. После имаме три вида на објекти: Tower (кула), Enemy (противнички брод) и Bullet (куршум). Тие ги наследуваат сите својства од Entity и дополнително содржат некои други како што се Demage, Range, Cost и ReloadTime за Tower; Health за Enemy; Target за Bullet и слично. Дополнително, класите Enemy и Bullet го имплементираат интерфејсот Movable кој го содржи методот Move() и им овозможува на тие инстанците од тие класи да се движат. Класите Tower и Enemy се апстрактни класи за од секоја да може да се создаваат различни типови со различни спецификации. Од класата Tower имаме изведено пет класи со различни јачини, а од Enemy имаме три. 

Во играта противничките бродови напаѓаат во бранови и секој бран содржи различен број од секој тип противници. Ова е имплементирано со класата Wave која во конструкторот прима бројот на противници од секој тип и ги иницијализира на соодветните позиции.

Главниот циклус на играта се повикува со помош на Timer кој на секој свој tick повикува метод Update кој ги прави потребните промени на позиции и состојби во играта. 
	
Текот на играта би изгледал вака: 
Се кеираат противнички бродови на почетокот на патеката и со секој tick на тајмерот ти се поместуваат се поблиску до својата цел. Со градење на кула покрај патеката таа со својот метод ChooseTarget го бира најблискиот брод и почнува да креира инстанции од Bullet упатени кон тој брод. При судир на Bullet со Enemy, животот на Enemy се намалува, а со тоа и неговата големина. Играта трае се додека сите инстанци од Enemy не се уништени.

###Заклучок

Space Wars е проект кој го сработивме во неколку дена, идејата ни дојде доста брзо, но самата имплементација потраја повеќе отколку што очекувавме, како и да е Space Wars на крај заврши како доста успешен проект. Иако времето беше кратко можеме со сигурност да речеме дека финалниот производ е задоволувачки. 
На страна од обврската како семинарска работа ние во изминатите неколку дена научивме многу нови работи, а она што веќе го знаевме успеавме успешно да го имплементираме. Научивме дека способноста за менаџирање со време е доста клучен фактор при изработката на вакви проекти, научивме дека знаењето од факултетските клупи не е доволно. Тоа е само добар вовед во она што доаѓа...

На кратко, ние со оваа семинарска работа стекнавме огромно знаење и искуство кое се надеваме дека ќе ни користи во иднина.
