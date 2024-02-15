data class Coffee(
    val name: String,
    val water: Int = 0,
    val milk: Int = 0,
    val beans: Int = 0,
)

class CoffeeMachine {
    private var water: Int = 0
    private var milk: Int = 0
    private var beans: Int = 0

    private val maxWater: Int = 2000
    private val maxMilk: Int = 1000
    private val maxBeans: Int = 500

    private lateinit var coffees: Map<String, Coffee>

    init {
        fill()
        loadCoffee()
    }

    private fun loadCoffee() {
        coffees = mapOf(
            "эспрессо" to Coffee("Эспрессо", 60, 0, 10),
            "американо" to Coffee("Американо", 120, 0, 10),
            "капучино" to Coffee("Капучино", 120, 60, 20),
            "латте" to Coffee("Латте", 240, 120, 20),
        )
    }

    private fun fill() {
        water = maxWater
        milk = maxMilk
        beans = maxBeans
    }

    private fun status() {
        println("$water мл воды\n$milk мл молока\n$beans гр кофе")
    }

    private fun cookCoffee(coffee: Coffee): Boolean {
        if (coffee.milk > milk) {
            println("Недостаточно молока!")
            return false
        }
        if (coffee.water > water) {
            println("Недостаточно воды!")
            return false
        }
        if (coffee.beans > beans) {
            println("Недостаточно кофе!")
            return false
        }
        water -= coffee.water
        milk -= coffee.milk
        beans -= coffee.beans
        println("${coffee.name} готов")
        return true
    }

    private fun cooking() {
        println("Введите название напитка или \"назад\" для возврата в главное меню")
        var cooking: Boolean = true
        var msg: String

        while (cooking) {
            println("Введите рецепт")
            msg = readln().lowercase()

            if (msg == "назад")
                break

            val coffee = coffees[msg]
            if (coffee != null)
                cooking = cookCoffee(coffee)
            else
                println("Рецепт не найден!")
        }
    }

    fun start() {
        println("Кофемашина готова к работе")
        var msg: String

        while (true) {
            println("Введите команду")
            msg = readln().lowercase()
            if (msg == "выключить") {
                println("До свидания!")
                break
            } else if (msg == "наполнить") {
                fill()
                println("Кофемашина наполнена")
            } else if (msg == "статус")
                status()
            else if (msg == "кофе")
                cooking()
            else
                println("Неизвестная команда")
        }

    }

}

fun main() {
    val machine = CoffeeMachine()
    machine.start()
}
