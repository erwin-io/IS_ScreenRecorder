
    let hours = 0;
    let minutes = 0;
    let seconds = 0;
    let timeStart = false;

    let download = {};

    let shouldStop = false;
    let stopped = false;
    let downloadFinish = false;
    let downloadButton = $('#download');
    let startRecordButton = $('#start-record');
    let stopRecordButton = $('#stop-record');
    let recordAgainButton = $('#record-again');
    let timeLabel = $('#time-label');
    let fileNameFormInput = $('#file-name-form-input');
    
    updateTimeLabel();
    
    startRecordButton.show();
    stopRecordButton.hide();
    downloadButton.hide();
    recordAgainButton.hide();
    timeLabel.hide();
    fileNameFormInput.hide();
    
    if (navigator.mediaDevices && navigator.mediaDevices.enumerateDevices) {
        // Firefox 38+ seems having support of enumerateDevicesx
        navigator.enumerateDevices = function(callback) {
            navigator.mediaDevices.enumerateDevices().then(callback);
        };
    }
    
    var MediaDevices = [];
    var isHTTPs = location.protocol === 'https:';
    var canEnumerate = false;
    
    if (typeof MediaStreamTrack !== 'undefined' && 'getSources' in MediaStreamTrack) {
        canEnumerate = true;
    } else if (navigator.mediaDevices && !!navigator.mediaDevices.enumerateDevices) {
        canEnumerate = true;
    }
    
    var hasMicrophone = false;
    var hasSpeakers = false;
    var hasWebcam = false;
    
    var isMicrophoneAlreadyCaptured = false;
    var isWebcamAlreadyCaptured = false;
    
    function checkDeviceSupport(callback) {
        if (!canEnumerate) {
            return;
        }
    
        if (!navigator.enumerateDevices && window.MediaStreamTrack && window.MediaStreamTrack.getSources) {
            navigator.enumerateDevices = window.MediaStreamTrack.getSources.bind(window.MediaStreamTrack);
        }
    
        if (!navigator.enumerateDevices && navigator.enumerateDevices) {
            navigator.enumerateDevices = navigator.enumerateDevices.bind(navigator);
        }
    
        if (!navigator.enumerateDevices) {
            if (callback) {
                callback();
            }
            return;
        }
    
        MediaDevices = [];
        navigator.enumerateDevices(function(devices) {
            devices.forEach(function(_device) {
                var device = {};
                for (var d in _device) {
                    device[d] = _device[d];
                }
    
                if (device.kind === 'audio') {
                    device.kind = 'audioinput';
                }
    
                if (device.kind === 'video') {
                    device.kind = 'videoinput';
                }
    
                var skip;
                MediaDevices.forEach(function(d) {
                    if (d.id === device.id && d.kind === device.kind) {
                        skip = true;
                    }
                });
    
                if (skip) {
                    return;
                }
    
                if (!device.deviceId) {
                    device.deviceId = device.id;
                }
    
                if (!device.id) {
                    device.id = device.deviceId;
                }
    
                if (!device.label) {
                    device.label = 'Please invoke getUserMedia once.';
                    if (!isHTTPs) {
                        device.label = 'HTTPs is required to get label of this ' + device.kind + ' device.';
                    }
                } else {
                    if (device.kind === 'videoinput' && !isWebcamAlreadyCaptured) {
                        isWebcamAlreadyCaptured = true;
                    }
    
                    if (device.kind === 'audioinput' && !isMicrophoneAlreadyCaptured) {
                        isMicrophoneAlreadyCaptured = true;
                    }
                }
    
                if (device.kind === 'audioinput') {
                    hasMicrophone = true;
                }
    
                if (device.kind === 'audiooutput') {
                    hasSpeakers = true;
                }
    
                if (device.kind === 'videoinput') {
                    hasWebcam = true;
                }
    
                // there is no 'videoouput' in the spec.
    
                MediaDevices.push(device);
            });
    
            if (callback) {
                callback();
            }
        });
    }
    
    function getSupporterd(){
    }

    checkDeviceSupport(getSupporterd);

    function startRecord() {
        startRecordButton.hide();
        stopRecordButton.show();
        downloadButton.hide();
        recordAgainButton.hide();
        timeLabel.show();
        hours = minutes = seconds = 0;
        updateTimeLabel();
        timeStart = true;
    }
    function stopRecord() {
        startRecordButton.hide();
        stopRecordButton.hide();
        downloadButton.show();
        fileNameFormInput.show();
        recordAgainButton.show();
        downloadButton.removeAttr("disabled");
        fileNameFormInput.addClass('pmd-textfield-floating-label-completed');
        timeStart = false;
        var tracks = window.MediaStreamTrack.getSources;
        var trackSrources = window.MediaStreamTrack.getSources;
        console.log(tracks);
        console.log(trackSrources);
    }
    const audioRecordConstraints = {
        echoCancellation: true
    }

    startRecordButton.on('click', function(){recordScreen();});
    stopRecordButton.on('click', function () {
        shouldStop = true;
    });
    // downloadButton.on('click', function(){
    //     if(fileNameValid()){
    //         e.preventDefault();  //stop the browser from following
    //         window.location.href = 'uploads/file.doc';
    //         downloadFinish = true;
    //     }
    // });
    fileNameFormInput.find('#file-name').on('keyup keypress blur change', function(){
        if(fileNameValid()){
            download.fileName = fileNameFormInput.find('#file-name').val();
            downloadButton.attr("href", download.href);
            downloadButton.attr("download", `${download.fileName}.webm`);
        }
    });
    recordAgainButton.on('click', function () {
        location.reload();
    });

    function fileNameValid() {
        if(fileNameFormInput.find('#file-name').val().length > 0){
            fileNameFormInput.removeClass( "has-error" );
            fileNameFormInput.find('.error-label').hide();
            let value = fileNameFormInput.find('#file-name').val();
            if (value.match(/^\s*[a-z-._\d,\s]+\s*$/i)) {
                fileNameFormInput.removeClass( "has-error" );
                fileNameFormInput.find('.error-label').hide();
                downloadButton.removeAttr("disabled");
                return true;
            } else {
                fileNameFormInput.find('#file-name').focus();
                fileNameFormInput.removeClass( "has-error" ).addClass( "has-error" );
                fileNameFormInput.find('.error-label').show();
                downloadButton.prop('disabled', true);
                return false;
            }
        }else{
            fileNameFormInput.removeClass( "has-error" ).addClass( "has-error" );
            fileNameFormInput.find('.error-label').show();
            downloadButton.prop('disabled', true);
            return false;
        }

    }

    const handleRecord = function ({stream, mimeType}) {
        startRecord()
        let recordedChunks = [];
        stopped = false;
        let mediaRecorder = new MediaRecorder(stream);

        mediaRecorder.ondataavailable = function (e) {
            if (e.data.size > 0) {
                recordedChunks.push(e.data);
            }

            if (shouldStop === true && stopped === false) {
                mediaRecorder.stop();
                stopped = true;
            }
        };

        mediaRecorder.onstop = function () {
            const blob = new Blob(recordedChunks, {
                type: mimeType
            });
            recordedChunks = []
			getSeekableBlob(blob, saveSeekable);
        };

        mediaRecorder.start(200);
    };
	
	function saveSeekable(blob){
        let date = new Date();
        // let month = d.getMonth()+1;
        // let day = d.getDate();
        // let date = d.getFullYear() + '-' +
        //     ((''+month).length<2 ? '0' : '') + month + '-' +
        //     ((''+day).length<2 ? '0' : '') + day;
        let formattedDate = date.getFullYear() + '-' +('0' + (date.getMonth()+1)).slice(-2)+ '-' +  ('0' + date.getDate()).slice(-2) + ' '+date.getHours()+ '-'+('0' + (date.getMinutes())).slice(-2)+ '-'+date.getSeconds();
        download = {
            href : URL.createObjectURL(blob),
            fileName : `recording-${formattedDate}`,
        };
        downloadButton.attr("href", download.href);
        downloadButton.attr("download", `${download.fileName}.webm`);
        fileNameFormInput.find('#file-name').val(download.fileName);
        stopRecord();
	}

    async function recordScreen() {
        const mimeType = 'video/webm';
        shouldStop = false;
        const constraints = {
            video: {
                cursor: 'motion'
            }
        };
        if(!(navigator.mediaDevices && navigator.mediaDevices.getDisplayMedia)) {
            return window.alert('Screen Record not supported!')
        }
        let stream = null;
        const audioContext = new AudioContext();
        
        
        if(hasMicrophone){
            const audioDestination = audioContext.createMediaStreamDestination();
            const voiceStream = await navigator.mediaDevices.getUserMedia({ audio: {'echoCancellation': true}, video: false });
            const userAudio = audioContext.createMediaStreamSource(voiceStream);
            userAudio.connect(audioDestination);
            
            const displayStream = await navigator.mediaDevices.getDisplayMedia({video: {cursor: "motion"}, audio: true});

            if(displayStream.getAudioTracks().length > 0) {
                const displayAudio = audioContext.createMediaStreamSource(displayStream);
                displayAudio.connect(audioDestination);
            }
    
            const tracks = [...displayStream.getVideoTracks(), ...audioDestination.stream.getTracks()];
            stream = new MediaStream(tracks);
        }
        else{
            const audioDestination = audioContext.createMediaStreamDestination();
            const displayStream = await navigator.mediaDevices.getDisplayMedia({video: {cursor: "motion"}, audio: true});

            if(displayStream.getAudioTracks().length > 0) {
                const displayAudio = audioContext.createMediaStreamSource(displayStream);
                displayAudio.connect(audioDestination);
            }
    
            const tracks = [...displayStream.getVideoTracks(), ...audioDestination.stream.getTracks()];
            stream = new MediaStream(tracks);
        }
        handleRecord({stream, mimeType});
        timeStart = true;
        setInterval(timerChange, 1000);

    }

    //decode data to enable seek and forward when playing webm file
    function getSeekableBlob(inputBlob, callback) {
        if (typeof EBML === 'undefined') {
            throw new Error('Please link: https://cdn.webrtc-experiment.com/EBML.js');
        }
        var reader = new EBML.Reader();
        var decoder = new EBML.Decoder();
        var tools = EBML.tools;
        var fileReader = new FileReader();
        fileReader.onload = function(e) {
            var ebmlElms = decoder.decode(this.result);
            ebmlElms.forEach(function(element) {
                reader.read(element);
            });
            reader.stop();
            var refinedMetadataBuf = tools.makeMetadataSeekable(reader.metadatas, reader.duration, reader.cues);
            var body = this.result.slice(reader.metadataSize);
            var newBlob = new Blob([refinedMetadataBuf, body], {
                type: 'video/webm'
            });
            callback(newBlob);
        };
        fileReader.readAsArrayBuffer(inputBlob);
    }
    function timerChange(){
        if(timeStart){
            updateTimeLabel();
            seconds++;
            if (seconds == 59) {
                minutes++;
                seconds = 0;
                if (minutes == 59) {
                    hours++;
                    minute = 0;
                }
            }
        }
    }
    function updateTimeLabel(){
        var timeFormatted = ("0" + hours).slice(-2) + " : " + ("0" + minutes).slice(-2) + " : " + ("0" + seconds).slice(-2);
        timeLabel.html(`RECORD ${timeFormatted}`);
    }