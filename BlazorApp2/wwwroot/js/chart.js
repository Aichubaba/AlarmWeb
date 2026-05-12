// Глобальная функция для создания графиков
window.createChart = (canvasId, chartData) => {
    const ctx = document.getElementById(canvasId);
    if (!ctx) {
        console.error(`Canvas with id ${canvasId} not found`);
        return;
    }

    // Удаляем старый график, если есть
    if (window.myChart) {
        window.myChart.destroy();
    }

    window.myChart = new Chart(ctx, chartData);
};
