/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.{html,razor}',
    ],
    theme: {
        extend: {
            colors: {
                primary: {
                    "1": "#065289",
                    "2": "#718ECD",
                    "3": "#AEB3B7",
                },
                secondary: {
                    "1": "#2D2006",
                    "2": "#896724",
                    "3": "#B29762",
                    "4": "#B6AD9A",
                }
            }
        },
    },
    plugins: [],
}

