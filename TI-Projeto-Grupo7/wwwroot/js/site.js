const form = document.querySelector('.php-email-form');
form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const loading = document.querySelector('.loading');
    const errorMessage = document.querySelector('.error-message');
    const sentMessage = document.querySelector('.sent-message');

    loading.style.display = 'block';

    try {
        const response = await fetch(form.action, {
            method: form.method,
            body: new FormData(form),
        });

        if (response.ok) {
            loading.style.display = 'none';
            sentMessage.style.display = 'block';
        } else {
            throw new Error('Erro na submissão');
        }
    } catch (error) {
        loading.style.display = 'none';
        errorMessage.textContent = error.message;
        errorMessage.style.display = 'block';
    }
});
