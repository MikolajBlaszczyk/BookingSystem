const { createProxyMiddleware } = require('http-proxy-middleware');


const context = [
    "/weatherforecast",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:5001',
        secure: false
    });

    app.use(
        '/api',
        createProxyMiddleware({
            target: "https://localhost:7012",
            changeOrigin: true,
            secure: false
        })
    )



    app.use(appProxy);
};
