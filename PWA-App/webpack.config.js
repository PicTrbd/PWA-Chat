var webpack = require('webpack');
var CopyWebpackPlugin = require('copy-webpack-plugin');
var path = require('path');

var BUILD_DIR = path.resolve(__dirname, 'build');
var APP_DIR = path.resolve(__dirname, 'src');

var config = {
  entry: APP_DIR + '/index.js',
  output: {
    path: BUILD_DIR,
    filename: 'bundle.js'
  },
  module : {
    loaders : [
      { test: /\.png$/, loader: "url-loader?mimetype=image/png" },      
      { test: /\.css$/, loader: "style-loader!css-loader" },      
      { test : /\.js?/, include : APP_DIR, loader : 'babel-loader' }
    ]
  },
  plugins: [
    new CopyWebpackPlugin([
        { from: './public/index.html', to: BUILD_DIR },
        { from: './public/manifest.json', to: BUILD_DIR },
        { from: './public/sw.js', to: BUILD_DIR },
        { from: './public/images/*', to: BUILD_DIR },      
        { from: './src/styles/App.css', to: BUILD_DIR }        
      ])]
};

module.exports = config;