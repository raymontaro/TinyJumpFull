FBInstant.initializeAsync()
.then(function() {
	console.log(window.FBInstant)
	FBInstant.setLoadingProgress(100);
	FBInstant.startGameAsync()
.then(function(){
	console.log('Game Started');
	game.start();
})
});

