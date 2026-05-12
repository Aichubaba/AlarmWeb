window.loginAndRedirect = async function (username, password, role) {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = '/api/auth/login';
    form.style.display = 'none';

    const addField = (name, value) => {
        const input = document.createElement('input');
        input.type = 'hidden';
        input.name = name;
        input.value = value;
        form.appendChild(input);
    };

    addField('username', username);
    addField('password', password);
    addField('role', role);

    document.body.appendChild(form);
    form.submit();
};
