public class RoomBookingSystem
{
    public void book(DateTime fromTo, DateTime toTo, int people, string typeBooking)
    {
        Console.WriteLine($"Бронирование номера с {fromTo.ToShortDateString()} по {toTo.ToShortDateString()} на {people} человек(а). Тип: {typeBooking}");
    }

    public void cancelBooking()
    {
        Console.WriteLine("Бронирование номера отменено.");
    }
}

public class CleaningService
{
    public void cleanRoom(int roomNumber)
    {
        Console.WriteLine($"Уборка номера {roomNumber} выполнена.");
    }

    public void notificationCleaning(int roomNumber, string message)
    {
        Console.WriteLine($"Уведомление об уборке для номера {roomNumber}: {message}");
    }

    public void checkOut(int roomNumber)
    {
        Console.WriteLine($"Выезд из номера {roomNumber}.");
    }
}

public class RestaurantSystem
{
    public void bookTable(int people, DateTime time)
    {
        Console.WriteLine($"Бронирование стола на {people} человек(а) на {time.ToShortTimeString()}.");
    }

    public void orderFood(int roomNumber, string foodOrder)
    {
        Console.WriteLine($"Заказ еды для номера {roomNumber}: {foodOrder}");
    }

    public void notificationKitchen(int people, int roomNumber)
    {
        Console.WriteLine($"Уведомление на кухню о заказе для номера {roomNumber} на {people} человек(а).");
    }
}

public class EventManagementSystem
{
    public void bookEvent(string eventName, DateTime eventDate, int participants)
    {
        Console.WriteLine($"Бронирование мероприятия '{eventName}' на {eventDate.ToShortDateString()} для {participants} участников.");
    }

    public void orderEquipment(string equipment)
    {
        Console.WriteLine($"Заказ оборудования: {equipment}");
    }
}

public class Notification
{
    public void sentNotification(string message)
    {
        Console.WriteLine($"Уведомление: {message}");
    }
}

public static class HotelFacade
{
    private static RoomBookingSystem roomBookingSystem = new RoomBookingSystem();
    private static CleaningService cleaningService = new CleaningService();
    private static RestaurantSystem restaurantSystem = new RestaurantSystem();
    private static EventManagementSystem eventManagementSystem = new EventManagementSystem();
    private static Notification notification = new Notification();

    public static void bookRoom(DateTime fromTo, DateTime toTo, int people, string typeBooking)
    {
        roomBookingSystem.book(fromTo, toTo, people, typeBooking);
        restaurantSystem.notificationKitchen(people, 215);
        notification.sentNotification("Бронирование номера успешно выполнено.");
    }

    public static void bookTableAndOrderFood(int people, DateTime time, int roomNumber, string foodOrder)
    {
        restaurantSystem.bookTable(people, time);
        restaurantSystem.orderFood(roomNumber, foodOrder);
        notification.sentNotification("Бронирование стола и заказ еды успешно выполнены.");
    }

    public static void organizeEvent(string eventName, DateTime eventDate, int participants)
    {
        eventManagementSystem.bookEvent(eventName, eventDate, participants);
        notification.sentNotification("Мероприятие успешно организовано.");
    }

    public static void cancelRoomBooking()
    {
        roomBookingSystem.cancelBooking();
        notification.sentNotification("Бронирование номера отменено.");
    }

    public static void requestCleaning(int roomNumber)
    {
        cleaningService.cleanRoom(roomNumber);
        notification.sentNotification($"Уборка для номера {roomNumber} выполнена.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        DateTime checkIn = new DateTime(2024, 12, 3);
        DateTime checkOut = new DateTime(2024, 12, 7);
        HotelFacade.bookRoom(checkIn, checkOut, 2, "Стандарт");

        HotelFacade.bookTableAndOrderFood(2, new DateTime(2024, 12, 4, 19, 0, 0), 215, "Паста и вино");

        HotelFacade.organizeEvent("Конференция", new DateTime(2024, 12, 5), 50);

        HotelFacade.cancelRoomBooking();

        HotelFacade.requestCleaning(215);
    }
}
