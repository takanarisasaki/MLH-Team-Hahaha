//
//  ViewController.swift
//  mlh-ios
//
//  Created by Chen He on 2018-12-01.
//  Copyright © 2018 Chen He. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    @IBAction func test() {
        var data = ["username":"ttttttttt","password":"sdfjsdfsfsd"]
        HTTP.post(addr: "users/login", data: data)
        
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }


}

