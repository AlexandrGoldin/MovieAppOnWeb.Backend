﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApp.Infrastructure.Entities;

namespace MovieApp.Infrastructure.Data
{
    public class ApplicationContextSeed()
    {
        public static async Task SeedAsync(ApplicationContext applicationContext,
            ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (applicationContext.Database.IsSqlServer())
                {
                    applicationContext.Database.Migrate();
                }

                if (!await applicationContext.Countries.AnyAsync())
                {
                    await applicationContext.Countries.AddRangeAsync(
                        GetPreconfiguredCountries());

                    await applicationContext.SaveChangesAsync();
                }

                if (!await applicationContext.Genres.AnyAsync())
                {
                    await applicationContext.Genres.AddRangeAsync(
                        GetPreconfiguredGenres());

                    await applicationContext.SaveChangesAsync();
                }

                if (!await applicationContext.Movies.AnyAsync())
                {
                    await applicationContext.Movies.AddRangeAsync(
                        GetPreconfiguredMovies());

                    await applicationContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 23) throw;

                retryForAvailability++;
                logger.LogError(ex.Message);
                await SeedAsync(applicationContext, logger, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<Country> GetPreconfiguredCountries()
        {
            return new List<Country>()
                {
                        new("США"),
                        new("Франция"),
                        new("Россия"),
                        new("Южная Корея"),
                        new("Германия")
                };
        }

        static IEnumerable<Genre> GetPreconfiguredGenres()
        {
            return new List<Genre>()
                {
                        new("Комедия"),
                        new("Боевик"),
                        new("Мелодрама"),
                        new("Драма"),
                        new("Детектив"),
                        new("Триллер")
                };
        }

        static IEnumerable<Movie> GetPreconfiguredMovies()
        { 
            return new List<Movie>() 
                {
                        new("Сбежавшая невеста", "Ричард Гир и Джулия Робертс вновь встречаются на экране после культовой Красотки. Перед их героями стоит нелёгкая задача — найти и не потерять любовь.", "Она — местная знаменитость, получившая неприятную славу взбалмошной сердцеедки из-за привычки сбегать от женихов со свадьбы. Он — опытный репортёр, намеренный написать про неудачливую невесту разоблачительную статью.", "https://localhost:7212/images/1.Сбежавшая невеста.png", "12+", 6.6m, DateOnly.Parse("1999-01-01"), 1, 3),
                        new("Амели", "Французская романтическая комедийная драма с обворожительной Одри Тоту.", "Молодая девушка Амели Пулен живёт в Париже в своём собственном сказочном мире. В детстве девочке совсем не уделяли времени, а после смерти матери отец и вовсе отдалился, поэтому Амели научилась развлекать себя сама и замечать что-то волшебное в обыденных вещах.", "https://localhost:7212/images/2.Амели.png", "16+", 7.5m, DateOnly.Parse("2001-01-01"), 2, 1),
                        new("Невезучие", "Французская криминальная комедия с Жераром Депардье и Жаном Рено", "Преступники Квентин и Руби – два совершенно разных, но одинаково невезучих парня. Наивный здоровяк Квентин мечтает стать жокеем, а пока – грабит банки и угоняет машины. Профессиональный киллер Руби – настоящий мастер своего дела.", "https://localhost:7212/images/3.Невезучие.png", "12+", 8.1m, DateOnly.Parse("2003-01-01"), 2, 1),
                        new("Ангел пролетел", "Это остросюжетная мелодрама о запретной любви, снятая по мотивам романа Ганны Слуцки.", "В провинциальном городе живёт молодая красивая девушка по имени Полина, которую в народе открыто называют ведьмой. «Ведьма» на самом деле народный целитель.", "https://localhost:7212/images/4.Ангел пролетел.png", "12+", 7.6m, DateOnly.Parse("2004-01-01"), 3, 3),
                        new("Разрешите тебя поцеловать", "Это остросюжетная мелодрама о запретной любви, снятая по мотивам романа Ганны Слуцки.", "Майор Григорий Власов только что пережил настоящее любовное разочарование, которое привело его к дарке с вышестоящим офицером.", "https://localhost:7212/images/5.Разрешите тебя поцеловать.png", "16+", 6.3m, DateOnly.Parse("2008-01-01"), 3, 3),
                        new("Всегда", "Трогательный мелодраматический фильм, повествующий о настоящей любви, преданности и верности.", "Бывший боксер Чанг Марселино старается забыть свое криминальное прошлое и пытается зарабатывать честным путем.", "https://localhost:7212/images/6.Всегда.png", "18+", 8.1m, DateOnly.Parse("2011-01-01"), 4, 2),
                        new("Зачетный препод", "Комедия положений о преступнике, который становится лучшим учителем для самых проблемных учеников.", "Зеки Мюллер – далеко не образец для подражания: он только освободился из тюрьмы и теперь хочет забрать награбленные деньги, чтобы продолжить свою неправедную жизнь.", "https://localhost:7212/images/7.Зачетный препод.png", "16+", 7.9m, DateOnly.Parse("2013-01-01"), 5, 1),
                        new("Лекарь-Ученик Авиценны", "Историческая драма по мотивам романа Ноя Гордона.", "Действие фильма начинается в 1021 году в Англии. Молодой парень по имени Роб рано потерял маму из-за неизлечимой болезни. Паренек прибился к путешествующему по городам шарлатану Барберу.", "https://localhost:7212/images/8.Лекарь-Ученик Авиценны.png", "18+", 7.8m, DateOnly.Parse("2013-01-01"), 5, 4),
                        new("Патерсон", "Меланхоличная трагикомедия Джима Джармуша о молодом водителе автобуса, который на досуге пишет стихи.", "Адам Драйвер играет главного героя по фамилии Патерсон, который живёт в городе Патерсон и водит автобус. У него есть красавица-жена Лора, и его жизнь внешне не нагружена событиями.", "https://localhost:7212/images/9.Патерсон.png", "18+", 7.4m, DateOnly.Parse("2016-01-01"), 1, 4),
                        new("Мышеловка на три персоны", "Любимый муж Кати Валентин Кряквин возвращается наконец-то домой из долгой экспедиции.", "Лифт в подъезде никак не могут починить, поэтому Валик поднимается пешком с большим и тяжелым чемоданом. На одной из лестничных клеток он замечает приоткрытую дверь и, желая сообщить об этом соседке, заходит внутрь.", "https://localhost:7212/images/10.Мышеловка на три персоны.png", "12+", 5.9m, DateOnly.Parse("2017-01-01"), 3, 5),
                        new("Проект Флорида", "Проект Флорида – независимая драма о жителях небольшого мотеля, расположенного недалеко от Диснейленда.", "Главная героиня, 6-летняя девочка Муни, живёт вместе с бесшабашной и безответственной молодой матерью, которая пытается заработать на жизнь различными, иногда незаконными, способами.", "https://localhost:7212/images/11.Проект Флорида.png", "16+", 6.7m, DateOnly.Parse("2017-01-01"), 1, 4),
                        new("Квадрат", "Сатирическая драма о кураторе музея современного искусства.", "Главный герой, Кристиан, готовит к открытию новую инсталляцию под названием «Квадрат» – очерченное на площади перед музеем пространство, где каждый может получить помощь, если нуждается.", "https://localhost:7212/images/12.Квадрат.png", "18+", 7.9m, DateOnly.Parse("2017-01-01"), 5, 4),
                        new("Про любовь Только для взрослых", "Почти половина российских браков распадается. В некоторых европейских странах этот показатель еще выше.", "Что мешает людям в современном обществе стать счастливыми? Рост благосостояния? Развитие интернета? Эмансипация? На все эти вопросы отвечает известный американский психолог, приехавший в Россию с лекцией.", "https://localhost:7212/images/13.Про любовь Только для взрослых.png", "18+", 7.0m, DateOnly.Parse("2017-01-01"), 3, 1),
                        new("Догмен", "Даг, лучше всех в мире понимающий собак, попадает за решётку.", "Что мешает людям в современном обществе стать счастливыми? Рост благосостояния? Развитие интернета? Эмансипация? На все эти вопросы отвечает известный американский психолог, приехавший в Россию с лекцией.", "https://localhost:7212/images/14.Догмен.png", "18+", 6.3m, DateOnly.Parse("2018-01-01"), 2, 6),
                        new("Счастье в конверте", "Трогательная мелодраматическая картина об истинных ценностях — любви, дружбе и взаимопомощи.", "Фильм состоит из трёх новелл, объединённых общим лейтмотивом — бумажными письмами, которые отправляют друг другу герои.", "https://localhost:7212/images/15.Счастье в конверте.png", "18+", 8.3m, DateOnly.Parse("2019-01-01"), 3, 3),
                        new("Кокаиновый барон", "Криминальный боевик, главные роли в котором исполнили голливудские актеры Николас Кейдж и Лоренс Фишбёрн.", "Грузовик, перевозивший серьезную партию кокаина из Мексики в Канаду, был атакован с вертолета неизвестной группой людей. Запрещенный товар исчезает.", "https://localhost:7212/images/16.Кокаиновый барон.png", "18+", 7.7m, DateOnly.Parse("2019-01-01"), 1, 3),
                        new("Виктория", "Российская семейная комедия про боксёра, внезапно ставшего отцом.", "Чемпион Европы по боям без правил Кирилл был настоящим мачо, до тех пор, пока в его дверь не постучался незваный гость.", "https://localhost:7212/images/17.Виктория.png", "16+", 7.8m, DateOnly.Parse("2021-01-01"), 3, 1),
                        new("Отряд Таганок", "В маленький аул приезжает передвижной кинотеатр, в котором показывают популярный фильм «Тимур и его команда».", "Вдохновившись увиденным, четверо местных мальчишек решают организовать свой отряд и выдвигаются на спасение величественной горы Кирамет.", "https://localhost:7212/images/18.Отряд Таганок.png", "6+", 8.2m, DateOnly.Parse("2021-01-01"), 3, 1),
                        new("Кабинет путешественника", "Ирина Владимировна и Роман Дмитриевич Куницыны возглавляют закрытую секретную исследовательскую лабораторию под названием «Кабинет путешественника»", "В лаборатории не только работают над различными лекарствами и вакцинами, там лечат людей с экзотическими заболеваниями, которые те приобрели во время путешествий. Отсюда и такое название — Кабинет путешественника.", "https://localhost:7212/images/19.Кабинет путешественника.png", "16+", 7.2m, DateOnly.Parse("2022-01-01"), 3, 5),
                        new("Сыщицы", "Алла Баринова – своенравная и пробивная оперативница убойного отдела МВД.", "Женя Алехина – её полная противоположность, тихая и застенчивая, погружённая в текст собственной диссертации о психологии убийц.", "https://localhost:7212/Images/20.Сыщицы.png", "18+", 6.9m, DateOnly.Parse("2022-01-01"), 3, 5),
                        new("Следующая жертва", "Остросюжетный корпоративный триллер, удостоившийся семиминутных оваций на Каннском кинофестивале.", "Юная студентка Со-хи устраивается на стажировку в колл-центр крупной компании. Родители и учителя девушки в восторге — закрепиться на таком месте, когда в стране процветает безработица, им кажется большой удачей.", "https://localhost:7212/images/21.Следующая жертва.png", "18+", 6.6m, DateOnly.Parse("2022-01-01"), 4,5),
                        new("Невидимый убийца", "В семье доктора Чон Тэ Хуна большая беда — его маленький сын попадает в больницу с внезапным фиброзом лёгких.", "Днём позднее от той же болезни погибает жена героя, и вскоре становится ясно: эти симптомы появляются у людей по всей Южной Корее.", "https://localhost:7212/images/22.Невидимый убийца.png", "18+", 7.8m, DateOnly.Parse("2022-01-01"), 4, 6)
            };           
        }
    }
}

